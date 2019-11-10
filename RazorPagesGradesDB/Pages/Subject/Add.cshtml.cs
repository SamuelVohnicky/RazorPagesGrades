using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesGrades.Services;
using RazorPagesGrades.ViewModels;
using RazorPagesGrades.Models;

namespace RazorPagesGrades.Pages.Subject
{
    public class AddModel : PageModel
    {
        readonly ISubjectManipulator _subj;
        public AddModel(ISubjectManipulator subj)
        {
            _subj = subj;
        }

        [TempData]
        public string MessageSuccess { get; set; }

        [TempData]
        public string MessageError { get; set; }

        [BindProperty]
        public SubjectViewModel Subject { get; set; }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (_subj.AddSubject(new Models.Subject() { Acronym = Subject.Acronym, Name = Subject.Name }))
                MessageSuccess = "Subject saved (" + Subject.Acronym + ")";
            else
                MessageError = $"Subject don't created - {Subject.Acronym} already exists!";

            return RedirectToPage("./Index");
        }
    }
}