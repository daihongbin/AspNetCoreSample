using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Util;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }

        public string DateSort { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public PaginatedList<Student> Student { get;set; }

        public async Task OnGetAsync(string sortOrder,string currentFilter,string searchString,int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";//互斥
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Student> studentIQ = from s in _context.Student
                                            select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                //studentIQ = studentIQ.Where(w => w.LastName.Contains(searchString) || w.FirstMidName.Contains(searchString));
                studentIQ = from sIQ in studentIQ
                            let upperSearchStr = searchString.ToUpper()
                            where sIQ.LastName.ToUpper().Contains(upperSearchStr)
                            || sIQ.FirstMidName.ToUpper().Contains(upperSearchStr)
                            select sIQ;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentIQ = studentIQ.OrderByDescending(od => od.LastName);
                    break;
                case "Date":
                    studentIQ = studentIQ.OrderBy(oa => oa.EnrollmentDate);
                    break;
                case "date_desc":
                    studentIQ = studentIQ.OrderByDescending(od => od.EnrollmentDate);
                    break;
                default:
                    studentIQ = studentIQ.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            Student = await PaginatedList<Student>.CreateAsync(studentIQ.AsNoTracking(),pageIndex ?? 1,pageSize);
        }
    }
}
