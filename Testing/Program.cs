using BLL.SubjectHandling.Concrete;
using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors;
using BLL.SubjectHandling.Processors.Concrete;
using BLL.SubjectHandling.Processors.Interface;
using BLL.Sys.Concrete;
using BLL.Sys.Interface;
using BLL.Sys.Processors;
using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;
using DAL.Enum.Sys;
using DbL.DatabaseHandler;
using static System.Net.Mime.MediaTypeNames;


namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DatabaseConfiger.Invoke_DirectoriesBuilders();
            //DatabaseConfiger.Invoke_DirectoriesCreators();
            //DatabaseConfiger.Console_ConnectionStrings();

            var initializer = new DatabaseInitializer();
            try
            {
                initializer.InitializeDatabases();
                Console.WriteLine("Databases initialized successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing databases: {ex.Message}");
            }

        }
    }
}


/* Test Session_Creation:
 
//ISessionBuilder SessionBuilder;
//SessionBuilder = new SessionBuilder();
//var S = SessionBuilder.generateSession(19, "This is MyCom.Info");
//Console.WriteLine(S.ToString());
//S.Dispose();
//Console.WriteLine(S.ToString());
//Console.WriteLine(SessionBuilder.ToString());
//SessionBuilder.Dispose();
//Console.WriteLine(SessionBuilder.ToString());

//GC.Collect();
//GC.WaitForPendingFinalizers();
//GC.Collect();

//// Measure memory before creating instances
//long beforeClass = GC.GetTotalMemory(true);
//Question q = new Question();
//long afterClass = GC.GetTotalMemory(true);
//Console.WriteLine($"Memory used by Question (class): {afterClass - beforeClass} bytes");

//// Measure memory before creating struct instance
//long beforeStruct = GC.GetTotalMemory(true);
//QuestionProperties qStruct = new QuestionProperties();
//long afterStruct = GC.GetTotalMemory(true);
//Console.WriteLine($"Memory used by QuestionProperties (struct): {afterStruct - beforeStruct} bytes");

*/

/* Test User_Creation:
            IUserProcessor userProcessor = new UserProcessor();
            IUserBuilder userBuilder = new UserBuilder(userProcessor);
            UserRole userRole = UserRole.Admin;
            userBuilder.BuildUser(19, "Mahmoud Gamaal Mesbah Mohamed", "Arafaw1999@gmail.com", "IndraMadara", userRole);
            var User = userBuilder.GetUser();
            Console.WriteLine(User.ToString());
 */

/* Test Question_Creation:
 
 
 
 
 
 
 
 */

/* Test Database Initialization:
            DataBase.InitializeDirectories();
            DataBase.InitializeDatabaseFilePaths();
            DataBase.InitializeConnectionStrings();
            DataBase.Excute();
 */

/* Test SubjectHandling Module:
 * ---------------------------*
 __Subject Creation:
{
            ISubjectProcessor _subjectProcessor = new SubjectProcessor();
            ISubjectBuilder _subjectBuilder = new SubjectBuilder(_subjectProcessor);
            _subjectBuilder.BuildSubject(1, 19, 2, "SUB-1", "Subject_Name", "This is 1st Subject I created.");
            Console.WriteLine(_subjectProcessor.ToString());
            _subjectProcessor.Dispose();
            Console.WriteLine(_subjectProcessor.ToString());
}
* ---------------------------*
 */

/* Good Formate for Date: 
  {//Console.WriteLine(DateTime.UtcNow.ToString());}
 */

/* Test: Question's Creation
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Set Question Creation Configuration:
            IQuestionProcessor _QuProcessor = new QuestionsProcessor();
            IQuestionBuilder _QuBuilder = new QuestionBuilder(_QuProcessor);
            List<Question> QuList = new List<Question>();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_19:
            _QuProcessor.setID(19);
            _QuProcessor.setSubjectID(1919);
            _QuProcessor.setType(QuestionType.TrueFalse);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.VeryEasy);
            _QuProcessor.setAnswerList(new List<string> { "True", "False" });
            _QuProcessor.setCorrectAnswer("True");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_20:
            _QuProcessor.setID(20);
            _QuProcessor.setSubjectID(2020);
            _QuProcessor.setType(QuestionType.MCQ);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.Easy);
            _QuProcessor.setAnswerList(new List<string> { "Ans_1", "Ans_2", "Ans_3", "Ans_4" });
            _QuProcessor.setCorrectAnswer("Ans_4");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////



            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_21:
            _QuProcessor.setID(21);
            _QuProcessor.setSubjectID(2121);
            _QuProcessor.setType(QuestionType.Essay);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.Difficult);
            _QuProcessor.setAnswerList(new List<string> { "Ans_Eassy" });
            _QuProcessor.setCorrectAnswer("Ans_Eassy");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_22
            _QuProcessor.setID(22);
            _QuProcessor.setSubjectID(2222);
            _QuProcessor.setType(QuestionType.TrueFalse);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.VeryDifficult);
            _QuProcessor.setAnswerList(new List<string> { "True", "False" });
            _QuProcessor.setCorrectAnswer("Ans_4");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_23
            _QuProcessor.setID(23);
            _QuProcessor.setSubjectID(2323);
            _QuProcessor.setType(QuestionType.MCQ);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.NA);
            _QuProcessor.setAnswerList(new List<string> { "Ans_1", "Ans_2", "Ans_3", "Ans_4" });
            _QuProcessor.setCorrectAnswer("Ans_1");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_24
            _QuProcessor.setID(24);
            _QuProcessor.setSubjectID(2424);
            _QuProcessor.setType(QuestionType.Essay);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.VeryEasy);
            _QuProcessor.setAnswerList(new List<string> { "Ans_Eassy" });
            _QuProcessor.setCorrectAnswer("Ans_Eassy");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_25
            _QuProcessor.setID(25);
            _QuProcessor.setSubjectID(2525);
            _QuProcessor.setType(QuestionType.TrueFalse);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.Easy);
            _QuProcessor.setAnswerList(new List<string> { "True", "False" });
            _QuProcessor.setCorrectAnswer("True");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Qu_26
            _QuProcessor.setID(26);
            _QuProcessor.setSubjectID(2626);
            _QuProcessor.setType(QuestionType.MCQ);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.Difficult);
            _QuProcessor.setAnswerList(new List<string> { "Ans_1", "Ans_2", "Ans_3", "Ans_4" });
            _QuProcessor.setCorrectAnswer("Ans_2");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            _QuProcessor.setID(20);
            _QuProcessor.setSubjectID(2020);
            _QuProcessor.setType(QuestionType.Essay);
            _QuProcessor.setText("Is this Question's Creation test?");
            _QuProcessor.setDifficultyLevel(QuestionDifficultyLevel.Difficult);
            _QuProcessor.setAnswerList(new List<string> { "Ans_Eassy" });
            _QuProcessor.setCorrectAnswer("Ans_Eassy");

            QuList.Add(_QuProcessor.getQuestion().Clone());
            _QuProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            foreach (var Qu in QuList)
            {
                Console.WriteLine(Qu.ReturnQuestionDetails());
            }
 
 
 
 
 */

/* Unit_Test: Creation of Subject 
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<Subject> _subjList = new List<Subject>();
            ISubjectProcessor _subjProcessor = new SubjectProcessor(); 
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_1:
            _subjProcessor.setID(1);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#1");
            _subjProcessor.setName("1st_Subject");
            _subjProcessor.setDescription("This is the first Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_2:
            _subjProcessor.setID(2);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#2");
            _subjProcessor.setName("2nd_Subject");
            _subjProcessor.setDescription("This is the Second Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 21, 22, 23, 24, 25, 26, 27, 28, 29, 210 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_3:
            _subjProcessor.setID(3);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#3");
            _subjProcessor.setName("3rd_Subject");
            _subjProcessor.setDescription("This is the Third Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 31, 32, 33, 34, 35, 36, 37, 38, 39, 310 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_4:
            _subjProcessor.setID(4);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#4");
            _subjProcessor.setName("4th_Subject");
            _subjProcessor.setDescription("This is the Fourth Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 41, 42, 43, 44, 45, 46, 47, 48, 49, 410 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_5:
            _subjProcessor.setID(5);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#5");
            _subjProcessor.setName("5th_Subject");
            _subjProcessor.setDescription("This is the Fifth Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 51, 52, 53, 54, 55, 56, 57, 58, 59, 510 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_6:
            _subjProcessor.setID(6);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#6");
            _subjProcessor.setName("6th_Subject");
            _subjProcessor.setDescription("This is the Sixth Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 61, 62, 63, 64, 65, 66, 67, 68, 69, 610 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_7:
            _subjProcessor.setID(7);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#7");
            _subjProcessor.setName("7th_Subject");
            _subjProcessor.setDescription("This is the Seventh Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 71, 72, 73, 74, 75, 76, 77, 78, 79, 710 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_8:
            _subjProcessor.setID(8);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#8");
            _subjProcessor.setName("8th_Subject");
            _subjProcessor.setDescription("This is the Eighth Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 81, 82, 83, 84, 85, 86, 87, 88, 89, 810 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_9:
            _subjProcessor.setID(9);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#9");
            _subjProcessor.setName("9th_Subject");
            _subjProcessor.setDescription("This is the Ninth Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 91, 92, 93, 94, 95, 96, 97, 98, 99, 910 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Subj_10:
            _subjProcessor.setID(10);
            _subjProcessor.setInstructorID(19);
            _subjProcessor.setCodeID("Subject#10");
            _subjProcessor.setName("10th_Subject");
            _subjProcessor.setDescription("This is the Tenth Subject I Created!");
            _subjProcessor.setQuestionsBankIDs(new List<int> { 101, 102, 103, 104, 105, 106, 107, 108, 109, 1010 });
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            _subjList.Add(_subjProcessor.getSubject().Clone());
            _subjProcessor.Terminate();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            foreach(var Subj in _subjList)
            {
                Console.WriteLine(Subj.ReturnSubjectDetails());
            }
 */

/* Unit_Test: Creation of Test(Entitiy):
    
            //int _id, int _testbankid,string _title, string _duration, bool _ispublished, List< int > _questionsids
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Initialize Test Lists:
            List<Test> testList_Clones = new List<Test>();
            List<Test> testList_Origins = new List<Test>();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_1:
            Test T_1 = new Test(1, 11, "Test#1", "This is the First Test I created!", false, new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 19, 110 });
            testList_Clones.Add(T_1.Clone());
            T_1.Dispose();
            testList_Origins.Add(T_1);
            //T_1.ReturnTestDetails();
            T_1 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test2_:
            Test T_2 = new Test(2, 22, "Test#2", "This is the Second Test I created!", true, new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 210 });
            testList_Clones.Add(T_2.Clone());
            T_2.Dispose();
            testList_Origins.Add(T_2);
            //T_2.ReturnTestDetails();
            T_2 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_3:
            Test T_3 = new Test(3, 33, "Test#3", "This is the Third Test I created!", false, new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 310 });
            testList_Clones.Add(T_3.Clone());
            T_3.Dispose();
            testList_Origins.Add(T_3);
            //T_3.ReturnTestDetails();
            T_3 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_4:
            Test T_4 = new Test(4, 44, "Test#4", "This is the Fourth Test I created!", true, new List<int>() { 41, 42, 43, 44, 45, 46, 47, 48, 49, 410 });
            testList_Clones.Add(T_4.Clone());
            T_4.Dispose();
            testList_Origins.Add(T_4);
            //T_4.ReturnTestDetails();
            T_4 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_5:
            Test T_5 = new Test(5, 55, "Test#5", "This is the Fifth Test I created!", false, new List<int>() { 51, 52, 53, 54, 55, 56, 57, 58, 59, 510 });
            testList_Clones.Add(T_5.Clone());
            T_5.Dispose();
            testList_Origins.Add(T_5);
            //T_5.ReturnTestDetails();
            T_5 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_6:

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            Test T_6 = new Test(6, 66, "Test#6", "This is the Sixth Test I created!", true, new List<int>() { 61, 62, 63, 64, 65, 66, 67, 68, 69, 610 });
            testList_Clones.Add(T_6.Clone());
            T_6.Dispose();
            testList_Origins.Add(T_6);
            //T_6.ReturnTestDetails();
            T_6 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_7:
            Test T_7 = new Test(7, 77, "Test#7", "This is the Seventh Test I created!", false, new List<int>() { 71, 72, 73, 74, 75, 76, 77, 78, 79, 710 });
            testList_Clones.Add(T_7.Clone());
            T_7.Dispose();
            testList_Origins.Add(T_7);
            //T_7.ReturnTestDetails();
            T_7 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_8:
            Test T_8 = new Test(8, 88, "Test#8", "This is the Eighth Test I created!", true, new List<int>() { 81, 82, 83, 84, 85, 86, 87, 88, 89, 810 });
            testList_Clones.Add(T_8.Clone());
            T_8.Dispose();
            testList_Origins.Add(T_8);
            //T_8.ReturnTestDetails();
            T_8 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_9:
            Test T_9 = new Test(9, 99, "Test#9", "This is the Ninth Test I created!", false, new List<int>() { 91, 92, 93, 94, 95, 96, 97, 98, 99, 910 });
            testList_Clones.Add(T_9.Clone());
            T_9.Dispose();
            testList_Origins.Add(T_9);
            //T_9.ReturnTestDetails();
            T_9 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_10:
            Test T_10 = new Test(10, 1010, "Test#10", "This is the Tenth Test I created!", true, new List<int>() { 101, 102, 103, 104, 105, 106, 107, 108, 109, 1010 });
            testList_Clones.Add(T_10.Clone());
            T_10.Dispose();
            testList_Origins.Add(T_10);
            //T_10.ReturnTestDetails();
            T_10 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Display The Tests' details(Befaore Disposing):
            foreach (Test T in testList_Clones)
            {
                Console.WriteLine(T.ReturnTestDetails());

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Display The Tests' details(After Disposing):
            foreach (Test T in testList_Origins)
            {
                Console.WriteLine(T.ReturnTestDetails());

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    
 */

/* Unit_Test: Creation of Test(Entity) By Using (TestProcessor):
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Initialize Test Lists:
            List<Test> testList_Clones = new List<Test>();
            List<Test> testList_Origins = new List<Test>();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Configure TestProcessor:
            ITestProcessor _testProcessor = new TestProcessor();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Test_1:  //1, 11, "Test#1", "This is the First Test I created!, Its Dusration is 1 Hour", false, new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 19, 110 });
            _testProcessor.setID(1);
            _testProcessor.setTestBankID(11);
            _testProcessor.setTitle("Test#1");
            _testProcessor.setDuration("This is the First Test I created!, Its Duration is 1 Hour");
            _testProcessor.setIsPublished(false);
            _testProcessor.setQuestionsIDs(new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 19, 110 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_2:  //2, 22, "Test#2", "This is the Second Test I created!", true, new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 210 };
            _testProcessor.setID(2);
            _testProcessor.setTestBankID(22);
            _testProcessor.setTitle("Test#2");
            _testProcessor.setDuration("This is the Second Test I created!, Its Duration is 2 Hours");
            _testProcessor.setIsPublished(true);
            _testProcessor.setQuestionsIDs(new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 210 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_3:  //3, 33, "Test#3", "This is the Third Test I created!", false, new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 310 });
            _testProcessor.setID(3);
            _testProcessor.setTestBankID(33);
            _testProcessor.setTitle("Test#3");
            _testProcessor.setDuration("This is the Third Test I created!, Its Duration is 3 Hours");
            _testProcessor.setIsPublished(false);
            _testProcessor.setQuestionsIDs(new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 310 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_4:  //4, 44, "Test#4", "This is the Fourth Test I created!", true, new List<int>() { 41, 42, 43, 44, 45, 46, 47, 48, 49, 410 }
            _testProcessor.setID(4);
            _testProcessor.setTestBankID(44);
            _testProcessor.setTitle("Test#4");
            _testProcessor.setDuration("This is the Fourth Test I created!, Its Duration is 4 Hours");
            _testProcessor.setIsPublished(true);
            _testProcessor.setQuestionsIDs(new List<int>() { 41, 42, 43, 44, 45, 46, 47, 48, 49, 410 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_5:  //5, 55, "Test#5", "This is the Fifth Test I created!", false, new List<int>() { 51, 52, 53, 54, 55, 56, 57, 58, 59, 510 } 
            _testProcessor.setID(55);
            _testProcessor.setTestBankID(55);
            _testProcessor.setTitle("Test#5");
            _testProcessor.setDuration("This is the Fifth Test I created!, Its Duration is 5 Hours");
            _testProcessor.setIsPublished(false);
            _testProcessor.setQuestionsIDs(new List<int>() { 51, 52, 53, 54, 55, 56, 57, 58, 59, 510 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_6:  //6, 66, "Test#6", "This is the Sixth Test I created!", true, new List<int>() { 61, 62, 63, 64, 65, 66, 67, 68, 69, 610 }
            _testProcessor.setID(6);
            _testProcessor.setTestBankID(66);
            _testProcessor.setTitle("Test#6");
            _testProcessor.setDuration("This is the Sixth Test I created!, Its Duration is 6 Hours");
            _testProcessor.setIsPublished(true);
            _testProcessor.setQuestionsIDs(new List<int>() { 61, 62, 63, 64, 65, 66, 67, 68, 69, 610 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_7:  //7, 77, "Test#7", "This is the Seventh Test I created!", false, new List<int>() { 71, 72, 73, 74, 75, 76, 77, 78, 79, 710 }
            _testProcessor.setID(7);
            _testProcessor.setTestBankID(7);
            _testProcessor.setTitle("Test#7");
            _testProcessor.setDuration("This is the Seventh Test I created!, Its Duration is 7 Hours");
            _testProcessor.setIsPublished(false);
            _testProcessor.setQuestionsIDs(new List<int>() { 71, 72, 73, 74, 75, 76, 77, 78, 79, 710 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_8:  //8, 88, "Test#8", "This is the Eighth Test I created!", true, new List<int>() { 81, 82, 83, 84, 85, 86, 87, 88, 89, 810 }
            _testProcessor.setID(8);
            _testProcessor.setTestBankID(88);
            _testProcessor.setTitle("Test#8");
            _testProcessor.setDuration("This is the Eighth Test I created!, Its Duration is 8 Hours");
            _testProcessor.setIsPublished(true);
            _testProcessor.setQuestionsIDs(new List<int>() { 81, 82, 83, 84, 85, 86, 87, 88, 89, 810 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_9:  //9, 99, "Test#9", "This is the Ninth Test I created!", false, new List<int>() { 91, 92, 93, 94, 95, 96, 97, 98, 99, 910 }
            _testProcessor.setID(9);
            _testProcessor.setTestBankID(99);
            _testProcessor.setTitle("Test#9");
            _testProcessor.setDuration("This is the Ninth Test I created!, Its Duration is 9 Hours");
            _testProcessor.setIsPublished(false);
            _testProcessor.setQuestionsIDs(new List<int>() { 91, 92, 93, 94, 95, 96, 97, 98, 99, 910 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///Test_10:  //10, 1010, "Test#10", "This is the Tenth Test I created!", true, new List<int>() { 101, 102, 103, 104, 105, 106, 107, 108, 109, 1010 }
            _testProcessor.setID(10);
            _testProcessor.setTestBankID(1010);
            _testProcessor.setTitle("Test#10");
            _testProcessor.setDuration("This is the Tenth Test I created!, Its Duration is 10 Hours");
            _testProcessor.setIsPublished(true);
            _testProcessor.setQuestionsIDs(new List<int>() { 101, 102, 103, 104, 105, 106, 107, 108, 109, 1010 });

            testList_Clones.Add(_testProcessor.getTest().Clone());

            _testProcessor.Terminate();

            testList_Origins.Add(_testProcessor.getTest().Clone());
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Display The Tests' details(Befaore Disposing):
            foreach (Test T in testList_Clones)
            {
                Console.WriteLine(T.ReturnTestDetails()); 
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Display The Tests' details(After Disposing):
            foreach (Test T in testList_Origins)
            {
                Console.WriteLine(T.ReturnTestDetails());
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////// 

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///End Test:
            testList_Clones.Clear();
            testList_Clones = null;
            testList_Origins.Clear();
            testList_Origins = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            
 */

/* Unit_Test: Creation of Test(Entity) By Using (TestBuilder):
    
 */

/* Unit_Test: Create of TestBank(Entity)
    
    //int _id, int _instructorid, string _title, string _description, bool _isactive, List<int> _testsids
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Initialize Test Lists:
            List<TestsBank> testsBankList_Clones = new List<TestsBank>();
            List<TestsBank> testsBankList_Origins = new List<TestsBank>();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_1:
            TestsBank TsBk_1 = new TestsBank(1, 11, "TestsBank#1", "This is the First Testsbank I Created!", true, new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 19, 110 });
            testsBankList_Clones.Add(TsBk_1.Clone());
            TsBk_1.Dispose();
            testsBankList_Origins.Add(TsBk_1);
            //T_1.ReturnTestDetails();
            TsBk_1 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_2:
            TestsBank TsBk_2 = new TestsBank(2, 21, "TestsBank#2", "This is the Second Testsbank I Created!", true, new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 210 });
            testsBankList_Clones.Add(TsBk_2.Clone());
            TsBk_2.Dispose();
            testsBankList_Origins.Add(TsBk_2);
            //T_1.ReturnTestDetails();
            TsBk_2 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_3:
            TestsBank TsBk_3 = new TestsBank(1, 11, "TestsBank#1", "This is the Third Testsbank I Created!", true, new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 310 });
            testsBankList_Clones.Add(TsBk_3.Clone());
            TsBk_3.Dispose();
            testsBankList_Origins.Add(TsBk_3);
            //T_1.ReturnTestDetails();
            TsBk_3 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_4:
            TestsBank TsBk_4 = new TestsBank(1, 11, "TestsBank#1", "This is the Fourth Testsbank I Created!", true, new List<int>() { 41, 42, 43, 44, 45, 46, 47, 48, 49, 410 });
            testsBankList_Clones.Add(TsBk_4.Clone());
            TsBk_4.Dispose();
            testsBankList_Origins.Add(TsBk_4);
            //T_1.ReturnTestDetails();
            TsBk_4 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_5:
            TestsBank TsBk_5 = new TestsBank(1, 11, "TestsBank#1", "This is the Fifth Testsbank I Created!", true, new List<int>() { 51, 52, 53, 54, 55, 56, 57, 58, 59, 510 });
            testsBankList_Clones.Add(TsBk_5.Clone());
            TsBk_5.Dispose();
            testsBankList_Origins.Add(TsBk_5);
            //T_1.ReturnTestDetails();
            TsBk_5 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_6:
            TestsBank TsBk_6 = new TestsBank(1, 11, "TestsBank#1", "This is the Sixth Testsbank I Created!", true, new List<int>() { 61, 62, 63, 64, 65, 66, 67, 68, 69, 610 });
            testsBankList_Clones.Add(TsBk_6.Clone());
            TsBk_6.Dispose();
            testsBankList_Origins.Add(TsBk_6);
            //T_1.ReturnTestDetails();
            TsBk_6 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_1:
            TestsBank TsBk_7 = new TestsBank(1, 11, "TestsBank#1", "This is the Seventh Testsbank I Created!", true, new List<int>() { 71, 72, 73, 74, 75, 76, 77, 78, 79, 710 });
            testsBankList_Clones.Add(TsBk_7.Clone());
            TsBk_7.Dispose();
            testsBankList_Origins.Add(TsBk_7);
            //T_1.ReturnTestDetails();
            TsBk_7 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_8:
            TestsBank TsBk_8 = new TestsBank(1, 11, "TestsBank#1", "This is the Eighth Testsbank I Created!", true, new List<int>() { 81, 82, 83, 84, 85, 86, 87, 88, 89, 810 });
            testsBankList_Clones.Add(TsBk_8.Clone());
            TsBk_8.Dispose();
            testsBankList_Origins.Add(TsBk_8);
            //T_1.ReturnTestDetails();
            TsBk_8 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_1:
            TestsBank TsBk_9 = new TestsBank(1, 11, "TestsBank#1", "This is the Ninth Testsbank I Created!", true, new List<int>() { 91, 92, 93, 94, 95, 96, 97, 98, 99, 910 });
            testsBankList_Clones.Add(TsBk_9.Clone());
            TsBk_9.Dispose();
            testsBankList_Origins.Add(TsBk_9);
            //T_1.ReturnTestDetails();
            TsBk_9 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///TestsBank_1:
            TestsBank TsBk_10 = new TestsBank(1, 11, "TestsBank#1", "This is the Tenth Testsbank I Created!", true, new List<int>() { 101, 102, 103, 104, 105, 106, 107, 108, 109, 1010 });
            testsBankList_Clones.Add(TsBk_10.Clone());
            TsBk_10.Dispose();
            testsBankList_Origins.Add(TsBk_10);
            //T_1.ReturnTestDetails();
            TsBk_10 = null;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Display The Tests' details(Befaore Disposing):
            foreach (TestsBank TsBk in testsBankList_Clones)
            {
                Console.WriteLine(TsBk.ReturnTestsBankDetails());

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///Display The Tests' details(After Disposing):
            foreach (TestsBank TsBk in testsBankList_Origins)
            {
                Console.WriteLine(TsBk.ReturnTestsBankDetails());

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    
 */


/* Unit_Test: Create of QuestionsBank(Entity)*/

/* Unit_Test: Create of QuestionsBank(Entity) By Using (QuestionsBankProcessor)*/

/* Unit_Test: Create of QuestionsBank(Entity)By Using (QuestionsBankBuilder)*/

/* Unit_Test: Create of Question(Entity)*/

/* Unit_Test: Create of Question(Entity) By Using (QuestionsProcessor)*/

/* Unit_Test: Create of Question(Entity)By Using (QuestionBuilder)*/


/* Unit_Test: Create of TestsBank(Entity) */

/* Unit_Test: Create of (Entity) By Using (TestsBankProcessor) */

/* Unit_Test: Create of (Entity) By Using (TestsBankBuilder)*/

/* Unit_Test: Create of Test(Entity) */

/* Unit_Test: Create of Test(Entity) By Using (TestProcessor) */

/* Unit_Test: Create of Test(Entity) By Using (TestsBankBuilder)*/

/* Unit_Test: Create of Subject(Entity) */

/* Unit_Test: Create of (Entity) By Using (SubjectProcessor) */

/* Unit_Test: Create of (Entity) By Using (SubjectBuilder)*/

