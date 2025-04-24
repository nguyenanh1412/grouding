using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WEB_SHOW_WRIST_STRAP.Models.Entities;
using WEB_SHOW_WRIST_STRAP;

public class ListViewController : Controller
{
    private readonly DataPointContext _context;

    public ListViewController(DataPointContext context)
    {
        _context = context;
    }

    Dataconfig dataconfig = new Dataconfig();

    // Danh sách cụm cố định (hard-coded)
    private readonly Dictionary<int, List<int>> _clusters = new Dictionary<int, List<int>>
    {
        { 1, new List<int> { 1, 2, 3,4 } },       // Cụm 1: Line 1-3
        { 2, new List<int> {  5, 6,7,8,9 } },       // Cụm 2: Line 4-6
        { 3, new List<int> { 10} },       // Cụm 3: Line 7-9
        { 4, new List<int> { 11 } }         // Cụm 4: Line 10-11
    };
   

    public IActionResult Index(int IdPoint = 0, int IdLine = 0)
    {
        ViewBag.IdPoint = IdPoint;
        ViewBag.IdLine = IdLine;

        return View(dataconfig);
    }
    // Action Detail
    public IActionResult Detail(int IdLine = 0)
    {
        // Tìm cụm chứa IdLine
        var cluster = _clusters.FirstOrDefault(c => c.Value.Contains(IdLine));
        var linesInCluster = cluster.Value != null ? cluster.Value : new List<int> { IdLine }; // Fallback nếu không tìm thấy cụm

        ViewBag.IdCluster = cluster.Key; // IdCluster (có thể null nếu không tìm thấy)
        ViewBag.Lines = linesInCluster;  // Danh sách IdLine trong cụm
        ViewBag.IdLine = IdLine;         // IdLine gốc để hiển thị

        return View(dataconfig);
    }
    public IActionResult EditLayoutLine(int IdLine = 0)
    {
        var cluster = _clusters.FirstOrDefault(c => c.Value.Contains(IdLine));
        var linesInCluster = cluster.Value != null ? cluster.Value : new List<int> { IdLine };

        ViewBag.IdCluster = cluster.Key;
        ViewBag.Lines = linesInCluster;
        ViewBag.IdLine = IdLine;

        return View(dataconfig);
    }
    // Lấy danh sách điểm cho toàn bộ cụm
    [HttpGet]
    public JsonResult GetPointLine(int idLine)
    {
        var cluster = _clusters.FirstOrDefault(c => c.Value.Contains(idLine));
        var lines = cluster.Value != null ? cluster.Value : new List<int> { idLine };

        var points = _context.ListPoints
            .Where(p => lines.Contains(p.IdLine))
            .Select(p => new
            {
                p.IdPoint,
                p.NamePoint,
                p.Csstop,
                p.Cssleft,
                p.TopDetail,
                p.LeftDetail,
                p.IdLine
            }).ToList();

        return Json(points);
    }
    
    // Cập nhật trạng thái cho toàn bộ cụm
    [HttpGet]
    public JsonResult UpdateStatus(int idLine)
    {
        var cluster = _clusters.FirstOrDefault(c => c.Value.Contains(idLine));
        var lines = cluster.Value != null ? cluster.Value : new List<int> { idLine };

        var latestData = _context.DataNows
            .Where(d => lines.Contains(d.IdLine))
            .OrderByDescending(d => d.TimeCheck)
            .Select(d => new
            {
                d.IdPoint,
                d.TimeCheck,
                d.Value,
                d.MinSpect,
                d.MaxSpect,
                d.Alarm,
                d.IdLine
            })
            .ToList();

        return Json(latestData);
    }




    // Cập nhật vị trí điểm cho toàn bộ cụm
    [HttpPost]
    public IActionResult UpdatePointPositions([FromBody] List<ListPoint> points)
    {
        foreach (var point in points)
        {
            var existingPoint = _context.ListPoints
                .FirstOrDefault(p => p.IdPoint == point.IdPoint && p.IdLine == point.IdLine);
            if (existingPoint != null)
            {
                existingPoint.TopDetail = point.Csstop;  // Lưu vào TopDetail
                existingPoint.LeftDetail = point.Cssleft; // Lưu vào LeftDetail
            }
        }
        _context.SaveChanges();
        return Ok();
    }
   
    // Lấy log dữ liệu cho toàn bộ cụm
    [HttpGet]
    public JsonResult GetLogData(int idLine)
    {
        var cluster = _clusters.FirstOrDefault(c => c.Value.Contains(idLine));
        var lines = cluster.Value != null ? cluster.Value : new List<int> { idLine };

        var now = DateTime.Now;
        int currentMonth = now.Month;
        int currentYear = now.Year;
        var calendar = CultureInfo.InvariantCulture.Calendar;

        using (var db = new DataPointContext())
        {
            var data = db.TotalData
                .Where(d => lines.Contains(d.IdLine) && d.TimeCheck.Year == currentYear && d.TimeCheck.Month == currentMonth)
                .AsEnumerable()
                .Select(d => new
                {
                    d.TimeCheck,
                    d.IdPoint,
                    d.IdLine,
                    Week = calendar.GetWeekOfYear(d.TimeCheck, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                    d.Value
                })
                .ToList();

            var last4Weeks = data
                .Where(d => d.Week >= calendar.GetWeekOfYear(now.AddDays(-28), CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                .GroupBy(d => new { d.Week, d.IdPoint, d.IdLine })
                .Select(g => new
                {
                    Type = "Week",
                    Date = "W" + g.Key.Week.ToString(),
                    g.Key.IdPoint,
                    g.Key.IdLine,
                    AvgValue = g.Sum(d => d.Value)
                }).ToList();

            var last3Days = db.TotalData
                .Where(d => lines.Contains(d.IdLine) && d.TimeCheck >= now.AddDays(-3))
                .GroupBy(d => new { d.TimeCheck.Date, d.IdPoint, d.IdLine })
                .Select(g => new
                {
                    Type = "Day",
                    Date = g.Key.Date.ToString("yyyy-MM-dd"),
                    g.Key.IdPoint,
                    g.Key.IdLine,
                    AvgValue = g.Sum(d => d.Value)
                }).ToList();

            var currentMonthData = db.TotalData
                .Where(d => lines.Contains(d.IdLine) && d.TimeCheck.Month == currentMonth && d.TimeCheck.Year == currentYear)
                .GroupBy(d => new { d.TimeCheck.Year, d.TimeCheck.Month, d.IdPoint, d.IdLine })
                .Select(g => new
                {
                    Type = "Month",
                    Date = g.Key.Year + "-" + g.Key.Month.ToString("D2"),
                    g.Key.IdPoint,
                    g.Key.IdLine,
                    AvgValue = g.Sum(d => d.Value)
                }).ToList();

            var result = last3Days.Concat(last4Weeks).Concat(currentMonthData).ToList();
            return Json(result);
        }
    }
}