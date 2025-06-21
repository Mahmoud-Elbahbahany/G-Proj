using BLL.SubjectHandling.Concrete;
using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Concrete;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using DAL.Repository.Concrete;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Managers
{
    public static class SubjectManager
    {
        private static readonly SubjectRepository _repository = new SubjectRepository();
        private static readonly ISubjectProcessor _processor = new SubjectProcessor();
        private static readonly ISubjectBuilder _builder = new SubjectBuilder(_processor);

        public static IEnumerable<Subject> GetAllSubjects()
        {
            return _repository.GetAll();
        }

        public static Subject GetSubjectById(int id)
        {
            return _repository.GetById(id);
        }

        public static IEnumerable<Subject> GetSubjectsByInstructorId(int instructorId)
        {
            return _repository.GetByInstructorId(instructorId);
        }

        public static Subject GetSubjectByCode(string code)
        {
            return _repository.GetByCode(code);
        }

        public static void AddSubject(Subject subject)
        {
            _repository.Add(subject);
        }

        public static void UpdateSubject(Subject subject)
        {
            _repository.Update(subject);
        }

        public static void DeleteSubject(int id)
        {
            _repository.Delete(id);
        }

        public static Subject CreateSubject(int id, int instructorId, string codeId,
            string name, string description, List<int> questionsBankIDs)
        {
            return _builder
                .WithID(id)
                .WithInstructorID(instructorId)
                .WithCodeID(codeId)
                .WithName(name)
                .WithDescription(description)
                .WithQuestionsBankIDs(questionsBankIDs)
                .Build();
        }

        public static IEnumerable<Subject> SearchSubjects(string searchTerm, string searchBy)
        {
            var allSubjects = GetAllSubjects();
            switch (searchBy)
            {
                case "Code":
                    return allSubjects.Where(s => s.CodeID.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                case "Name":
                    return allSubjects.Where(s => s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                case "InstructorID":
                    if (int.TryParse(searchTerm, out int instructorId))
                        return allSubjects.Where(s => s.InstructorID == instructorId);
                    break;
            }
            return Enumerable.Empty<Subject>();
        }
    }
}