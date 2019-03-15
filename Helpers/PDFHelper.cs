using Codaxy.WkHtmlToPdf;
using ResumeStripper.Models;
using ResumeStripper.Models.Experiences;
using System;
using System.IO;
using System.Web;

namespace ResumeStripper.Helpers
{
    public class PDFHelper
    {
        private bool hasEducation = false;
        private bool hasWork = false;
        private bool hasCourse = false;
        private bool hasSideline = false;
        private bool hasLanguage = false;
        private bool hasSkill = false;
        private bool hasHobby = false;
        private bool hasReference = false;
        private bool hasCompetence = false;

        private const string EducationPiece =
            @"<div class=""EduRow"">
            <span><b>Name Education:</b> [EDUCATIONNAME]</span>
            <span><b>Level of Education:</b> [EDUCATIONLEVEL]</span>
            <span><b>Name Institute:</b> [INSTITUTENAME]</span>
            <span><b>Location Institute:</b> [INSTITUTELOCATION]</span>
            <span><b>Begin Date:</b> [EDUBEGIN]</span>
            <span><b>End Date:</b> [EDUEND]</span>
            <span><b>Diploma:</b> [EDUDIPLOMA]</span>
            </div>
            <br />
            [EROW]";

        private const string WorkPiece =
            @"<div class=""WorkRow"">
            <span><b>Job Title:</b> [WORKJOB]</span>
            <span><b>Name Company:</b> [COMPANYNAME]</span>
            <span><b>Location Company:</b> [COMPANYLOCATION]</span>
            <span><b>Task Description:</b> <br />[WORKDESCRIPTION]</span>
            <span><b>Begin Date:</b> [WORKBEGIN]</span>
            <span><b>End Date:</b> [WORKEND]</span>
            </div>
            <br />
            [WROW]";

        private const string CoursePiece =
            @"<div class=""CourseRow"">
            <span><b>Name Course:</b> [COURSENAME]</span>
            <span><b>Name Institute:</b> [COURSEINSTITUTENAME]</span>
            <span><b>Location Institute:</b> [COURSEINSTITUTELOCATION]</span>
            <span><b>Year:</b> [COURSEYEAR]</span>
            <span><b>Diploma:</b> [COURSECERTIFICATE]</span>
            </div>
            <br />
            [CROW]";

        private const string SidelinePiece =
            @"<div class=""SidelineRow"">
            <span><b>Job Title:</b> [SIDELINEJOB]</span>
            <span><b>Name Company:</b> [ORGANIZATIONNAME]</span>
            <span><b>Location Company:</b> [ORGANIZATIONLOCATION]</span>
            <span><b>Task Description:</b> <br />[SIDELINEDESCRIPTION]</span>
            <span><b>Begin Date:</b> [SIDELINEBEGIN]</span>
            <span><b>End Date:</b> [SIDELINEEND]</span>
            </div>
            <br />
            [SROW]";

        private const string LanguagePiece =
            @"<div class=""LanguageRow"">
            <span><b>Name:</b> [LANGUAGENAME]</span>
            <span><b>Level:</b> [LANGLEVEL]</span>
            <span><b>Level of Speaking:</b> [LANGSPEAK]</span>
            <span><b>Level of Listening:</b> [LANGLISTEN]</span>
            <span><b>Level of Writing:</b> [LANGWRITE]</span>
            </div>
            <br />
            [LROW]";

        private const string ReferencePiece =
            @"<div class=""ReferenceRow"">
            <span><b>Name:</b> [REFERENCENAME]</span>
            <span><b>Company Name:</b> [REFERENCECOMPANYNAME]</span>
            <span><b>Job Title:</b> [REFERENCEJOBTITLE]</span>
            <span><b>Emailaddress:</b> [REFERENCEEMAIL]</span>
            <span><b>Phone Number:</b> [REFERENCEPHONE]</span>
            </div>
            <br />
            [RROW]";

        private string skills = "";
        private string licenses = "";
        private string hobbies = "";
        private string competences = "";

        public byte[] GetPDF(string url, CV cv)
        {
            string template = getTemplate();

            //EDUCATIONS
            foreach (EducationExperience e in cv.Educations)
            {
                hasEducation = true;
                //add piece to template
                template = template.Replace("[EROW]", EducationPiece);

                //replace all parts with CV information
                template = template.Replace("[EDUCATIONNAME]", e.Name);
                template = template.Replace("[EDUCATIONLEVEL]", e.LevelOfEducation.ToString());
                template = template.Replace("[INSTITUTENAME]", e.OrganizationName);
                template = template.Replace("[INSTITUTELOCATION]", e.LocationOrganization);
                template = template.Replace("[EDUBEGIN]", e.BeginDate.Date.ToShortDateString());
                template = template.Replace("[EDUEND]", e.EndDate.Date.ToShortDateString());

                string result = "";

                if (e.Diploma)
                {
                    result = "Yes";
                }
                else
                {
                    result = "No";
                }
                template = template.Replace("[EDUDIPLOMA]", result);
            }

            //WORK EXPERIENCES
            foreach (WorkExperience e in cv.WorkExperiences)
            {
                hasWork = true;
                //add piece to template
                template = template.Replace("[WROW]", WorkPiece);

                //replace all parts with CV information
                template = template.Replace("[WORKJOB]", e.JobTitle);
                template = template.Replace("[COMPANYNAME]", e.OrganizationName);
                template = template.Replace("[COMPANYLOCATION]", e.LocationOrganization);
                template = template.Replace("[WORKDESCRIPTION]", e.TaskDescription);
                template = template.Replace("[WORKBEGIN]", e.BeginDate.Date.ToShortDateString());
                template = template.Replace("[WORKEND]", e.EndDate.Date.ToShortDateString());
            }

            //COURSES
            foreach (CourseExperience e in cv.Courses)
            {
                hasCourse = true;
                //add piece to template
                template = template.Replace("[CROW]", CoursePiece);

                //replace all parts with CV information
                template = template.Replace("[COURSENAME]", e.Name);
                template = template.Replace("[COURSEINSTITUTENAME]", e.OrganizationName);
                template = template.Replace("[COURSEINSTITUTELOCATION]", e.LocationOrganization);
                template = template.Replace("[COURSEYEAR]", e.Year.Date.Year.ToString());

                string result = "";

                if (e.Certificate)
                {
                    result = "Yes";
                }
                else
                {
                    result = "No";
                }
                template = template.Replace("[COURSECERTIFICATE]", result);
            }

            //SIDELINES
            foreach (SidelineExperience e in cv.SideLines)
            {
                hasSideline = true;
                //add piece to template
                template = template.Replace("[SROW]", SidelinePiece);

                //replace all parts with CV information
                template = template.Replace("[SIDELINEJOB]", e.JobTitle);
                template = template.Replace("[ORGANIZATIONNAME]", e.OrganizationName);
                template = template.Replace("[ORGANIZATIONLOCATION]", e.LocationOrganization);
                template = template.Replace("[SIDELINEDESCRIPTION]", e.TaskDescription);
                template = template.Replace("[SIDELINEBEGIN]", e.BeginDate.Date.ToShortDateString());
                template = template.Replace("[SIDELINEEND]", e.EndDate.Date.ToShortDateString());
            }

            //LANGUAGES
            foreach (Language e in cv.Languages)
            {
                hasLanguage = true;
                //add piece to template
                template = template.Replace("[LROW]", LanguagePiece);

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                //replace all parts with CV information
                template = template.Replace("[LANGUAGENAME]", e.Name);

                if (e.isSimple)
                {
                    string type = e.Level.ToString();
                    //simple mode used, only display Level

                    if (type.Equals("VeryGood"))
                    {
                        type = "Very Good";
                    }

                    template = template.Replace("[LANGLEVEL]", type);
                    //empty others
                    template = template.Replace(@"<p><b>Level of Speaking:</b> [LANGSPEAK]</p>", "");
                    template = template.Replace(@"<p><b>Level of Writing:</b> [LANGWRITE]</p>", "");
                    template = template.Replace(@"<p><b>Level of Listening:</b> [LANGLISTEN]</p>", "");
                }
                else
                {
                    //detailed mode used, only display seperate levels

                    string typeSpeak = e.LevelOfSpeaking.ToString();
                    string typeWrite = e.LevelOfWriting.ToString();
                    string typeListen = e.LevelOfListening.ToString();

                    if (typeSpeak.Equals("VeryGood"))
                    {
                        typeSpeak = "Very Good";
                    }
                    else if (typeWrite.Equals("VeryGood"))
                    {
                        typeWrite = "Very Good";
                    }
                    else if (typeListen.Equals("VeryGood"))
                    {
                        typeListen = "Very Good";
                    }

                    template = template.Replace("[LANGSPEAK]", typeSpeak);
                    template = template.Replace("[LANGLISTEN]", typeListen);
                    template = template.Replace("[LANGWRITE]", typeWrite);
                    //empty level
                    template = template.Replace(@"<p><b>Level:</b> [LANGLEVEL]</p>", "");
                }
            }

            //SKILLS
            foreach (Skill e in cv.Skills)
            {
                hasSkill = true;

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                skills += e.Name + ", ";
            }

            //removes spare ', '
            skills = skills.TrimEnd(' ');
            skills = skills.TrimEnd(',');

            template = template.Replace("[SKILLNAME]", skills);

            //REFERENCES
            foreach (Reference e in cv.References)
            {
                hasReference = true;
                //add piece to template
                template = template.Replace("[RROW]", ReferencePiece);

                //replace all parts with CV information
                template = template.Replace("[REFERENCENAME]", e.Name);
                template = template.Replace("[REFERENCECOMPANYNAME]", e.CompanyName);
                template = template.Replace("[REFERENCEJOBTITLE]", e.JobTitle);
                template = template.Replace("[REFERENCEEMAIL]", e.Email);
                template = template.Replace("[REFERENCEPHONE]", e.PhoneNumber);
            }

            //HOBBIES
            foreach (Hobby e in cv.Hobbies)
            {
                hasHobby = true;

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                hobbies += e.Name + ", ";
            }

            //removes spare ', '
            hobbies = hobbies.TrimEnd(' ');
            hobbies = hobbies.TrimEnd(',');

            template = template.Replace("[HOBBYNAME]", hobbies);

            //COMPETENCES
            foreach (Competence e in cv.Competences)
            {
                hasCompetence = true;

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                competences += e.Name + ", ";
            }

            //removes spare ', '
            competences = competences.TrimEnd(' ');
            competences = competences.TrimEnd(',');

            template = template.Replace("[COMPETENCENAME]", competences);

            if (!cv.IsAnonymous)
            {
                //NOT ANONYMOUS CV SO PRINT NAMES
                if (cv.Name != null && cv.Name != "")
                {
                    //capitalizes first letter
                    char[] a = cv.Name.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    cv.Name = new string(a);

                    template = template.Replace("[FIRST]", cv.Name);
                }
                else
                {
                    template = template.Replace(@"<span><b>First Name:</b></span>
                                                    <br />", "");
                    template = template.Replace(@"<span class=""answer"">[FIRST]</span>
                                                    <br />", "");
                }

                if (cv.Prefix != null && cv.Prefix != "")
                {
                    //capitalizes first letter
                    char[] a = cv.Prefix.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    cv.Prefix = new string(a);

                    template = template.Replace("[PRE]", cv.Prefix);
                }
                else
                {
                    template = template.Replace(@"<span><b>Prefix:</b></span>
                                                    <br />", "");
                    template = template.Replace(@"<span class=""answer"">[PRE]</span>
                                                    <br />", "");
                }

                if (cv.Surname != null && cv.Surname != "")
                {
                    //capitalizes first letter
                    char[] a = cv.Surname.ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    cv.Surname = new string(a);

                    template = template.Replace("[LAST]", cv.Surname);
                }
                else
                {
                    template = template.Replace(@"<span><b>Last Name:</b></span>
                                                    <br />", "");
                    template = template.Replace(@"<span class=""answer"">[LAST]</span>
                                                    <br />", "");
                }
            }
            else
            {
                //ANONYMOUS CV SO HIDE NAMES
                template = template.Replace(@"[FIRST] [PRE] [LAST]", "Curriculum vitae");
                template = template.Replace(@"<h4 class=""CVTitle""><i>Curriculum vitae</i></h4>", "");
                template = template.Replace(@"<span><b>First Name:</b></span>
                                                    <br />", "");
                template = template.Replace(@"<span class=""answer"">[FIRST]</span>
                                                    <br />", "");
                template = template.Replace(@"<span><b>Prefix:</b></span>
                                                    <br />", "");
                template = template.Replace(@"<span class=""answer"">[PRE]</span>
                                                    <br />", "");
                template = template.Replace(@"<span><b>Last Name:</b></span>
                                                    <br />", "");
                template = template.Replace(@"<span class=""answer"">[LAST]</span>
                                                    <br />", "");
            }

            //RESIDENCE
            if (cv.Residence != null && cv.Residence != "")
            {
                //capitalizes first letter
                char[] a = cv.Residence.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                cv.Residence = new string(a);

                template = template.Replace("[RESIDENCE]", cv.Residence);
            }
            else
            {
                template = template.Replace(@"<span><b>Residence:</b></span>
                                                    <br />", "");
                template = template.Replace(@"<span class=""answer"">[RESIDENCE]</span>
                                                    <br />", "");
            }

            //COUNTRY
            if (cv.Country != null && cv.Country != "")
            {
                //capitalizes first letter
                char[] a = cv.Country.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                cv.Country = new string(a);
                template = template.Replace("[COUNTRY]", cv.Country);
            }
            else
            {
                template = template.Replace(@"<span><b>Country:</b></span>
                                                    <br />", "");
                template = template.Replace(@"<span class=""answer"">[COUNTRY]</span>
                                                    <br />", "");
            }

            //DATE OF BIRTH
            if (cv.DateOfBirth != null && cv.DateOfBirth != DateTime.MinValue)
            {
                template = template.Replace("[DOB]", cv.DateOfBirth.Date.ToShortDateString());
            }
            else //if date equals 1/1/0001 00:00:00 AM. aka if there was no dob entered
            {
                template = template.Replace(@"<span><b>Date of Birth:</b></span>
                                                    <br />", "");
                template = template.Replace(@"<span class=""answer"">[DOB]</span>
                                                    <br />", "");
            }

            //LICENSES
            if (cv.Licenses.Count != 0)
            {
                //adds all licenses to 1 string
                foreach (License l in cv.Licenses)
                {
                    string type = l.Type.ToString();

                    if (type.Equals("Bplus"))
                    {
                        type = "B+";
                    }

                    licenses += type + ", ";
                }
                //removes extra spaces and comma's at end
                licenses = licenses.TrimEnd(' ');
                licenses = licenses.TrimEnd(',');

                template = template.Replace("[LICENSE]", licenses);
            }
            else
            {
                //there are no licenses
                template = template.Replace("[LICENSE]", "None");
            }

            //PROFILE
            if (cv.Profile != "" && cv.Profile != null)
            {
                //capitalizes first letter
                char[] a = cv.Profile.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                cv.Profile = new string(a);
                template = template.Replace("[PROFILE]", cv.Profile);
            }
            else
            {
                template = template.Replace(@"<br />
                                                <span><b>Profile:</b></span>
                                                <br />
                                                <span class=""answer"">[PROFILE]</span>", "");
            }

            //removes fields based on if elements exist or not
            if (!hasEducation)
            {
                template = template.Replace(@"<h2><b>Educations</b></h2>", "");
            }

            if (!hasWork)
            {
                template = template.Replace(@"<h2><b>Work Experiences</b></h2>", "");
            }

            if (!hasCourse)
            {
                template = template.Replace(@"<h2><b>Courses</b></h2>", "");
            }

            if (!hasSideline)
            {
                template = template.Replace(@"<h2><b>Sidelines</b></h2>", "");
            }

            if (!hasLanguage)
            {
                template = template.Replace(@"<h2><b>Languages</b></h2>", "");
            }

            if (!hasSkill)
            {
                template = template.Replace(@"<h2><b>Skills</b></h2>", "");
            }

            if (!hasHobby)
            {
                template = template.Replace(@"<h2><b>Hobbies</b></h2>", "");
            }

            if (!hasCompetence)
            {
                template = template.Replace(@"<h2><b>Competences</b></h2>", "");
            }

            if (!hasReference)
            {
                template = template.Replace(@"<h2><b>References</b></h2>", "");
            }

            template = template.Replace("[EROW]", "");
            template = template.Replace("[WROW]", "");
            template = template.Replace("[CROW]", "");
            template = template.Replace("[SROW]", "");
            template = template.Replace("[LROW]", "");
            template = template.Replace("[RROW]", "");

            //just cleanup of empty paragraphs if any
            template = template.Replace("<p></p>", "");
            template = template.Replace("<p> </p>", "");
            template = template.Replace("<p><b></b></p>", "");
            template = template.Replace("<p><b> </b></p>", "");

            string headUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Header.html");
            string footUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Footer.html");

            PdfConvert.ConvertHtmlToPdf(new Codaxy.WkHtmlToPdf.PdfDocument
            {
                Html = template,
                HeaderUrl = headUrl,
                FooterUrl = footUrl
            },
            new PdfOutput
            {
                OutputFilePath = url
            });

            return File.ReadAllBytes(url);
        }

        public string getTemplate()
        {
            string template = @"<html>
                                <link href=""https://fonts.googleapis.com/css?family=Open+Sans:400,700"" rel=""stylesheet"">
                                <style>html {color:#ff700d; font-family: 'Open Sans', sans-serif;} body{margin-top:150px;margin-bottom:50px;width:100%} h2 {font-size: 30px;} p, span {font-size: 18px;} h1 {font-size: 35px; white-space:nowrap;} .keep-together {page-break-inside: avoid;}.break-before {page-break-before: always;}.break-after {page-break-after: always;} .CVBigTitle {margin-top: -30px; max-width: 450px; color: #ff700d;} .CVSmallTitle {margin-top: 100px; max-width: 450px; color:#a9a9a9;} .answer {color: #a9a9a9;}
                                </style>
                                        <head>
                                        </head>
                                        <body>
                                            <br />
                                            <h3 class=""CVSmallTitle"">Curriculum vitae</h3>
                                            <h1 class=""CVBigTitle""><b>[FIRST] [PRE] [LAST]</b></h1>
                                            <hr />
                                            <br />
                                              <div class=""keep-together"">
                                                <h2><b>Personalia</b></h2>
                                                <div style=""float: left; width: 30%;"">
                                                    <span><b>First Name:</b></span>
                                                    <br />
                                                    <span><b>Prefix:</b></span>
                                                    <br />
                                                    <span><b>Last Name:</b></span>
                                                    <br />
                                                    <span><b>Residence:</b></span>
                                                    <br />
                                                    <span><b>Country:</b></span>
                                                    <br />
                                                    <span><b>Date of Birth:</b></span>
                                                    <br />
                                                    <span><b>Licenses:</b></span>
                                                    <br />
                                                </div>
                                                <div style=""float: left; width: 69%"">
                                                    <span class=""answer"">[FIRST]</span>
                                                    <br />
                                                    <span class=""answer"">[PRE]</span>
                                                    <br />
                                                    <span class=""answer"">[LAST]</span>
                                                    <br />
                                                    <span class=""answer"">[RESIDENCE]</span>
                                                    <br />
                                                    <span class=""answer"">[COUNTRY]</span>
                                                    <br />
                                                    <span class=""answer"">[DOB]</span>
                                                    <br />
                                                    <span class=""answer"">[LICENSE]</span>
                                                </div>
                                                <br />
                                                <br />
                                                <span><b>Profile:</b></span>
                                                <br />
                                                <span class=""answer"">[PROFILE]</span>
                                            </div>
                                            <div class=""keep-together"" id=""EducationDiv"">
                                                <h2><b>Educations</b></h2>
                                                [EROW]
                                            </div >
                                            <br />
                                            <div id=""WorkDiv"">
                                                <h2><b>Work Experiences</b></h2>
                                                [WROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""CourseDiv"">
                                                <h2><b>Courses</b></h2>
                                                [CROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""SidelineDiv"">
                                                <h2><b>Sidelines</b></h2>
                                                [SROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""LanguageDiv"">
                                                <h2><b>Languages</b></h2>
                                                [LROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""SkillDiv"">
                                                <h2><b>Skills</b></h2>
                                                <div class=""SkillRow"">
                                                <span>[SKILLNAME]</span>
                                                </div>
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""HobbyDiv"">
                                                <h2><b>Hobbies</b></h2>
                                                <div class=""HobbyRow"">
                                                <span>[HOBBYNAME]</span>
                                                </div>
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""CompetenceDiv"">
                                                <h2><b>Competences</b></h2>
                                                <div class=""CompetenceRow"">
                                                <span>[COMPETENCENAME]</span>
                                                </div>
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""ReferenceDiv"">
                                                  <h2><b>References</b></h2>
                                                 [RROW]
                                            </div>
                                        </body>
                                    </html>";
            return template;
        }
    }
}