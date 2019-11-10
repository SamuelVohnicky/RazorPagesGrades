using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPagesGrades.Models;
using RazorPagesGrades.ViewModels;

namespace RazorPagesGrades.Services
{
    public class CertificateTable : ICertificate
    {
        private SortedDictionary<string, Grade> _gradeTable = new SortedDictionary<string, Grade>();

        public bool AddGrade(Grade grade)
        {
            
            if (!_gradeTable.ContainsKey(grade.Subject.Acronym))
            {
                return _gradeTable.TryAdd(grade.Subject.Acronym, new Grade() { Id = Guid.NewGuid(), Subject = grade.Subject, Value = grade.Value * grade.Weight, Weight = grade.Weight });
            }
            else
            {
                _gradeTable[grade.Subject.Acronym].Value += grade.Value * grade.Weight;
                _gradeTable[grade.Subject.Acronym].Weight += grade.Weight;
                return true;
            }
        }

        public IEnumerable<GradeViewModel> GetCertificateTable()
        {
            return _gradeTable.Values.Select(gr => { return new GradeViewModel() { Acronym = gr.Subject.Acronym, Id = gr.Id, Subject = gr.Subject.Name, Value = gr.Value / gr.Weight, Weight = gr.Weight }; });
        }
    }
}
