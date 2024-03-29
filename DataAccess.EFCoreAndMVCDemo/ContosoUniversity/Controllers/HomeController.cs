﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in _context.Students
                group student by student.EnrollmentDate
                into dateGroup
                select new EnrollmentDateGroup
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };

            return View(await data.AsNoTracking().ToListAsync());
        }*/

        public async Task<ActionResult> About()
        {
            var groups = new List<EnrollmentDateGroup>();
            var conn = _context.Database.GetDbConnection();

            try
            {
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    var query = @"select EnrollmentDate,Count(*) as StudentCount
                                from Person
                                where Discriminator = 'Student'
                                group by EnrollmentDate";
                    command.CommandText = query;
                    var reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new EnrollmentDateGroup
                            {
                                EnrollmentDate = reader.GetDateTime(0),
                                StudentCount = reader.GetInt32(1)
                            };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();

                }
            }
            finally
            {
                conn.Close();
            }

            return View(groups);
        }
    }
}
