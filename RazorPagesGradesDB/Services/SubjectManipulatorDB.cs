using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesGrades.Models;

namespace RazorPagesGrades.Services
{
    public class SubjectManipulatorDB : ISubjectManipulator
    {
        readonly GradesContext _db;

        public SubjectManipulatorDB(GradesContext db)
        {
            _db = db;
        }

        public List<SelectListItem> SubjectListItems
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in _db.Subjects)
                {
                    list.Add(new SelectListItem() { Value = item.Acronym, Text = item.Acronym + " (" + item.Name + ")" });
                }
                return list;
            }
        }

        public bool AddSubject(Subject subject)
        {
            var temp = _db.Subjects.SingleOrDefault(s => s.Acronym == subject.Acronym.ToUpper());
            if (!(temp is null)) return false;
            subject.Acronym = subject.Acronym.ToUpper();
            _db.Subjects.Add(subject);
            _db.SaveChanges();
            return true;
        }

        public void SeedSubjects()
        {
            if (_db.Subjects.Count() > 0) return;
            _db.Subjects.Add(new Subject { Acronym = "MAT", Name = "Matematika" });
            _db.Subjects.Add(new Subject { Acronym = "PRG", Name = "Programování" });
            _db.Subjects.Add(new Subject { Acronym = "WEB", Name = "Webové aplikace" });
            _db.Subjects.Add(new Subject { Acronym = "TEV", Name = "Tělocvik" });
            _db.Subjects.Add(new Subject { Acronym = "CJL", Name = "Český jazyk a literatura" });
            _db.Subjects.Add(new Subject { Acronym = "ANJ", Name = "Anglický jazyk" });
            _db.SaveChanges();
        }
    }
}
