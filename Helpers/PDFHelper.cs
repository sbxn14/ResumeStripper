using PdfExtract;
using ResumeStripper.Models;
using ResumeStripper.Models.Experiences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace ResumeStripper.Helpers
{
    public class PdfHelper
    {
        public string FirstnameOptionalPrefixLastname =
            @"([A-Z][a-z'-]+)\s?([a-z'-]*\s?[a-z'-]*\s?[a-z'-]*\s?)?([A-Z][A-Za-z'-]+)";

        public bool HasEducation = false;
        public bool HasWork = false;
        public bool HasCourse = false;
        public bool HasSideline = false;
        public bool HasLanguage = false;
        public bool HasSkill = false;
        public bool HasHobby = false;
        public bool HasReference = false;
        public bool HasCompetence = false;

        private const string EducationPiece =
            @"<div class=""row EduRow keep-together"" style=""page-break-inside: avoid!important;"">
                  <div class=""column1edu"">
                    <span class=""answer""><b>[EDUBEGIN] - [EDUEND]</b></span>
                    <br />
                  </div>
                  <div class=""column2edu"">
                    <span><b>[EDUCATIONNAME]</b></span>
                    <span class=""answer""><br />[EDUCATIONLEVEL]
                    <br />[INSTITUTENAME], [INSTITUTELOCATION]</span>
                    <br /><span class=""answer"">[EDUDIPLOMA]</span>
                  </div>
            </div>
            <br />
            [EROW]";

        private const string WorkPiece =
            @"<div class=""row WorkRow keep-together"" style=""page-break-inside: avoid!important;"">
                  <div class=""column1edu"">
                    <span class=""answer""><b>[WORKBEGIN] - [WORKEND]</b></span>
                  </div>
                  <div class=""column2edu"">
                    <span><b>[WORKJOB]</b></span>
                    <br /><span class=""answer"">[COMPANYNAME], [COMPANYLOCATION]
                    <br />[WORKDESCRIPTION]</span>
                  </div>
              </div>
            <br />
            [WROW]";

        private const string CoursePiece =
            @"<div class=""row CourseRow keep-together"" style=""page-break-inside: avoid!important;"">
              <div class=""column1edu"" >
                <span class=""answer""><b>[COURSEYEAR]</b><span>
                <br />
              </div>
              <div class=""column2edu"" >
                <span><b>[COURSENAME]</b></span>
                <br />
                <span class=""answer"">[COURSEINSTITUTENAME], [COURSEINSTITUTELOCATION]</span>
                <br />
                <span class=""answer"">[COURSECERTIFICATE]</span>
              </div>
            </div>
            <br />
            [CROW]";

        private const string SidelinePiece =
            @"<div class=""row SidelineRow keep-together"" style=""page-break-inside: avoid!important;"">
                  <div class=""column1edu"" >
                    <span class=""answer""><b>[SIDELINEBEGIN] - [SIDELINEEND]</b></span>
                  </div>
                  <div class=""column2edu"">
                    <span><b>[SIDELINEJOB]</b></span>
                    <br /><span class=""answer"">[ORGANIZATIONNAME], [ORGANIZATIONLOCATION]
                    <br />[SIDELINEDESCRIPTION]</span>
                  </div>
              </div>
            <br />
            [SROW]";

        private const string LanguagePieceSimple =
            @"<div class=""row LanguageRow keep-together"" style=""page-break-inside: avoid!important;"">
                  <div class=""column1edu"">
                    <span><b>[LANGNAME]</b><span>
                  </div>
                  <div class=""column2edu"">
                    <span><b>Level</b><br /></span><span class=""answer"">[LANGLEVEL]</span>
                  </div>
             </div>
             <br />
             [LROW]";

        private const string LanguagePieceDetailed =
            @"<div class=""row LanguageRow keep-together"" style=""page-break-inside: avoid!important;"">
                  <div class=""columnlan"">
                    <span><b>[LANGNAME]</b><span>
                  </div>
                  <div class=""columnlan"">
                    <span><b>Level of Speaking</b><br /></span><span class=""answer"">[LANGSPEAK]</span>
                  </div>
                  <div class=""columnlan"">
                    <span><b>Level of Listening</b><br /></span><span class=""answer"">[LANGLISTEN]</span>
                  </div>
                  <div class=""columnlan"">
                    <span><b>Level of Writing</b><br /></span><span class=""answer"">[LANGWRITE]</span>
                  </div>
              </div>
              <br />
              [LROW]";

        private const string ReferencePiece =
            @"<div class=""row ReferenceRow keep-together"" style=""margin-bottom:130px;"" style=""page-break-inside: avoid!important;"">
                <div style=""float: left; width: 30%;"">
                    <span><b>Name:</b></span>
                    <br />
                    <span><b>Company Name:</b></span>
                    <br />
                    <span><b>Job Title:</b></span>
                    <br />
                    <span><b>Emailaddress:</b></span>
                    <br />
                    <span><b>Phone Number:</b></span>
                </div>
                <div style=""float: left; width: 69%"">
                    <span class=""answer"">[REFERENCENAME]</span>
                    <br />
                    <span class=""answer"">[REFERENCECOMPANYNAME]</span>
                    <br />
                    <span class=""answer"">[REFERENCEJOBTITLE]</span>
                    <br />
                    <span class=""answer"">[REFERENCEEMAIL]</span>
                    <br />
                    <span class=""answer"">[REFERENCEPHONE]</span>
                </div>
            </div>
            <br />
            [RROW]";

        public string Skills = "";
        public string Licenses = "";
        public string Hobbies = "";
        public string Competences = "";

        public CV ExtractData(string url)
        {
            CV result = new CV();
            string PDFText = "";
            //get plain text from PDF
            using (var pdfStream = File.OpenRead(url))
            using (var extractor = new Extractor())
            {
                PDFText = extractor.ExtractToString(pdfStream);
            }

            if (!PDFText.Equals(""))
            {
                //if extracted text is not empty
                //TODO Find way to extract relevant data
                string[] array = PDFText.Split(null);
                List<string> resultWords = new List<string>();

                foreach (string s in array)
                {
                    if (Regex.Match(s, FirstnameOptionalPrefixLastname).Success)
                    {
                        //if word matches the regex, add to list for further analyzing
                        resultWords.Add(s);
                    }
                }

                //TODO connection to database comparing the words.
            }
            else
            {
                //return that pdf is empty/has no text
                return null;
            }

            return result;
        }

        public byte[] GeneratePdf(string url, CV cv)
        {
            string template = GetTemplate();

            //capitalizes all relevant values in the different lists
            cv.CapitalizeLists();

            //EDUCATIONS
            foreach (EducationExperience e in cv.Educations)
            {
                HasEducation = true;
                //add piece to template
                template = template.Replace("[EROW]", EducationPiece);
                
                //replace all parts with CV information
                template = template.Replace("[EDUCATIONNAME]", e.Name);
                template = template.Replace("[EDUCATIONLEVEL]", e.LevelOfEducation);
                template = template.Replace("[INSTITUTENAME]", e.OrganizationName);
                template = template.Replace("[INSTITUTELOCATION]", e.LocationOrganization);
                template = template.Replace("[EDUBEGIN]", e.BeginDate.Date.ToShortDateString());

                DateTime current = DateTime.Today;
                //changes date to 'present' if current day is given
                if (current.ToString("M/d/yyyy").Equals(e.EndDate.Date.ToString("M/d/yyyy")))
                {
                    //given date is today
                    template = template.Replace("[EDUEND]", "present");
                }
                else
                {
                    template = template.Replace("[EDUEND]", e.EndDate.Date.ToShortDateString());
                }

                string result = "";

                result = e.Diploma ? "Diploma acquired" : "Diploma not acquired";

                template = template.Replace("[EDUDIPLOMA]", result);
            }

            //WORK EXPERIENCES
            foreach (WorkExperience e in cv.WorkExperiences)
            {
                HasWork = true;
                //add piece to template
                template = template.Replace("[WROW]", WorkPiece);

                //replace all parts with CV information
                template = template.Replace("[WORKJOB]", e.JobTitle);
                template = template.Replace("[COMPANYNAME]", e.OrganizationName);
                template = template.Replace("[COMPANYLOCATION]", e.LocationOrganization);
                template = template.Replace("[WORKDESCRIPTION]", e.TaskDescription);
                template = template.Replace("[WORKBEGIN]", e.BeginDate.Date.ToShortDateString());

                DateTime current = DateTime.Today;
                //changes date to 'present' if current day is given
                if (current.ToString("M/d/yyyy").Equals(e.EndDate.Date.ToString("M/d/yyyy")))
                {
                    //given date is today
                    template = template.Replace("[WORKEND]", "present");
                }
                else
                {
                    template = template.Replace("[WORKEND]", e.EndDate.Date.ToShortDateString());
                }
            }

            //COURSES
            foreach (CourseExperience e in cv.Courses)
            {
                HasCourse = true;
                //add piece to template
                template = template.Replace("[CROW]", CoursePiece);

                //replace all parts with CV information
                template = template.Replace("[COURSENAME]", e.Name);
                template = template.Replace("[COURSEINSTITUTENAME]", e.OrganizationName);
                template = template.Replace("[COURSEINSTITUTELOCATION]", e.LocationOrganization);
                template = template.Replace("[COURSEYEAR]", e.Year.Date.Year.ToString());

                string result = "";

                result = e.Certificate ? "Certificate acquired" : "Certificate not acquired";

                template = template.Replace("[COURSECERTIFICATE]", result);
            }

            //SIDELINES
            foreach (SidelineExperience e in cv.SideLines)
            {
                HasSideline = true;
                //add piece to template
                template = template.Replace("[SROW]", SidelinePiece);

                //replace all parts with CV information
                template = template.Replace("[SIDELINEJOB]", e.JobTitle);
                template = template.Replace("[ORGANIZATIONNAME]", e.OrganizationName);
                template = template.Replace("[ORGANIZATIONLOCATION]", e.LocationOrganization);
                template = template.Replace("[SIDELINEDESCRIPTION]", e.TaskDescription);
                template = template.Replace("[SIDELINEBEGIN]", e.BeginDate.Date.ToShortDateString());

                DateTime current = DateTime.Today;
                //changes date to 'present' if current day is given
                if (current.ToString("M/d/yyyy").Equals(e.EndDate.Date.ToString("M/d/yyyy")))
                {
                    //given date is today
                    template = template.Replace("[SIDELINEEND]", "present");
                }
                else
                {
                    template = template.Replace("[SIDELINEEND]", e.EndDate.Date.ToShortDateString());
                }
            }

            //LANGUAGES
            foreach (Language e in cv.Languages)
            {
                HasLanguage = true;

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                if (e.IsSimple)
                {
                    string type = e.Level.ToString();
                    //simple mode used, only display Level

                    //add piece to template
                    template = template.Replace("[LROW]", LanguagePieceSimple);

                    if (type.Equals("VeryGood"))
                    {
                        type = "Very Good";
                    }

                    template = template.Replace("[LANGLEVEL]", type);
                }
                else
                {
                    //detailed mode used, only display seperate levels
                    string typeSpeak = e.LevelOfSpeaking.ToString();
                    string typeWrite = e.LevelOfWriting.ToString();
                    string typeListen = e.LevelOfListening.ToString();

                    //add piece to template
                    template = template.Replace("[LROW]", LanguagePieceDetailed);

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
                }

                //replace language name
                template = template.Replace("[LANGNAME]", e.Name);
            }

            //SKILLS
            foreach (Skill e in cv.Skills)
            {
                HasSkill = true;

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                Skills += e.Name + ", ";
            }

            //removes spare ', '
            Skills = Skills.TrimEnd(' ');
            Skills = Skills.TrimEnd(',');

            template = template.Replace("[SKILLNAME]", Skills);

            //REFERENCES
            foreach (Reference e in cv.References)
            {
                HasReference = true;
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
                HasHobby = true;

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                Hobbies += e.Name + ", ";
            }

            //removes spare ', '
            Hobbies = Hobbies.TrimEnd(' ');
            Hobbies = Hobbies.TrimEnd(',');

            template = template.Replace("[HOBBYNAME]", Hobbies);

            //COMPETENCES
            foreach (Competence e in cv.Competences)
            {
                HasCompetence = true;

                //capitalizes first letter
                char[] a = e.Name.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                e.Name = new string(a);

                Competences += e.Name + ", ";
            }

            //removes spare ', '
            Competences = Competences.TrimEnd(' ');
            Competences = Competences.TrimEnd(',');

            template = template.Replace("[COMPETENCENAME]", Competences);

            if (!cv.IsAnonymous)
            {
                //NOT ANONYMOUS CV SO PRINT NAMES
                if (!string.IsNullOrEmpty(cv.Name))
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

                if (!string.IsNullOrEmpty(cv.Prefix))
                {
                    template = template.Replace("[PRE]", cv.Prefix);
                }
                else
                {
                    template = template.Replace(@"
                                                        <span><b>Prefix:</b></span>
                                                        <br />", "");
                    template = template.Replace(@"
                                                        <span class=""answer"">[PRE]</span>
                                                        <br />", "");
                    template = template.Replace(@" [PRE] ", " ");
                }

                if (!string.IsNullOrEmpty(cv.Surname))
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
            if (!string.IsNullOrEmpty(cv.Residence))
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
            if (!string.IsNullOrEmpty(cv.Country))
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

                    Licenses += type + ", ";
                }

                //removes extra spaces and comma's at end
                Licenses = Licenses.TrimEnd(' ');
                Licenses = Licenses.TrimEnd(',');

                template = template.Replace("[LICENSE]", Licenses);
            }
            else
            {
                //there are no licenses
                template = template.Replace("[LICENSE]", "None");
            }

            //PROFILE
            if (!string.IsNullOrEmpty(cv.Profile))
            {
                //capitalizes first letter
                char[] a = cv.Profile.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                cv.Profile = new string(a);
                template = template.Replace("[PROFILE]", cv.Profile);
            }
            else
            {
                template = template.Replace(@"<div class=""keep-together"">
                                                    <h3 style=""margin-top:130px!important""><b>Profile</b></h3>
                                                    <span class=""answer"" style=""margin-top: 25px;"">[PROFILE]</span>
                                                </div>", "");
            }

            //removes fields based on if elements exist or not
            if (!HasEducation)
            {
                template = template.Replace(@"<h2><b>Educations</b></h2>", "");
            }

            if (!HasWork)
            {
                template = template.Replace(@"<h2><b>Work Experiences</b></h2>", "");
            }

            if (!HasCourse)
            {
                template = template.Replace(@"<h2><b>Courses</b></h2>", "");
            }

            if (!HasSideline)
            {
                template = template.Replace(@"<h2><b>Sidelines</b></h2>", "");
            }

            if (!HasLanguage)
            {
                template = template.Replace(@"<h2><b>Languages</b></h2>", "");
            }

            if (!HasSkill)
            {
                template = template.Replace(@"<h2><b>Skills</b></h2>", "");
            }

            if (!HasHobby)
            {
                template = template.Replace(@"<h2><b>Hobbies</b></h2>", "");
            }

            if (!HasCompetence)
            {
                template = template.Replace(@"<h2><b>Competences</b></h2>", "");
            }

            if (!HasReference)
            {
                template = template.Replace(@"<h2><b>References</b></h2>", "");
            }

            template = template.Replace("[EROW]", "");
            template = template.Replace("[WROW]", "");
            template = template.Replace("[CROW]", "");
            template = template.Replace("[SROW]", "");
            template = template.Replace("[LROW]", "");
            template = template.Replace("[RROW]", "");

            //just cleanup of empty paragraphs and spans if any
            template = template.Replace("<p></p>", "");
            template = template.Replace("<p> </p>", "");
            template = template.Replace("<p><b></b></p>", "");
            template = template.Replace("<p><b> </b></p>", "");
            template = template.Replace("<span></span>", "");
            template = template.Replace("<span> </span>", "");
            template = template.Replace("<span><b></b></span>", "");
            template = template.Replace("<span><b> </b></span>", "");

            string headUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Header.html");
            string footUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Footer.html");

            PdfConvert.ConvertHtmlToPdf(new PdfDocument
            {
                Html = template,
                HeaderUrl = headUrl,
                FooterUrl = footUrl,
            },
                new PdfOutput
                {
                    OutputFilePath = url
                });

            return File.ReadAllBytes(url);
        }

        private string GetTemplate()
        {
            string template = @"<html>
                                <link href=""https://fonts.googleapis.com/css?family=Open+Sans:400,700"" rel=""stylesheet"">
                                <style>*{box-sizing: border-box;}.column1edu {float: left;width: 25%;padding: 10px;}.column2edu {float: left;width: 75%;padding: 10px;}.columnlan {float: left;width: 25%;padding: 10px;}.row:after {content: "";display: table;clear: both;}html {color:#ff700d; font-family: 'Open Sans', sans-serif;} body{margin-top:150px;margin-bottom:50px;width:100%} h2 {font-size: 30px;} p, span {font-size: 18px;} h1 {font-size: 35px; white-space:nowrap;} .keep-together {page-break-inside: avoid!important;}.break-before {page-break-before: always;}.break-after {page-break-after: always;} .CVBigTitle {max-width: 450px; color: #ff700d; margin-bottom:-10px;} .CVSmallTitle {margin-top: 100px; margin-bottom:-33px; max-width: 450px; color:#a9a9a9;} .answer {color: #a9a9a9;}
                                </style>
                                        <head>
                                        </head>
                                        <body>
                                            <br />
                                            <h3 class=""CVSmallTitle"">Curriculum vitae</h3>
                                            <h1 class=""CVBigTitle""><b>[FIRST] [PRE] [LAST]</b></h1>
                                            <hr />
                                                <h2><b>Personalia</b></h2>
                                                <div class=""keep-together"">
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
                                                </div>
                                                <br />
                                                <div class=""keep-together"">
                                                    <h3 style=""margin-top:130px!important""><b>Profile</b></h3>
                                                    <span class=""answer"" style=""margin-top: 25px;"">[PROFILE]</span>
                                                </div>
                                            <br />
                                            <div class=""keep-together"" id=""EducationDiv"" style=""margin-top:50px!important;"">
                                                <h2><b>Educations</b></h2>
                                                [EROW]
                                            </div >
                                            <br />
                                            <div class=""keep-together"" id=""WorkDiv"">
                                                <h2><b>Work Experiences</b></h2>
                                                [WROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together"" id=""CourseDiv"">
                                                <h2><b>Courses</b></h2>
                                                [CROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together"" id=""SidelineDiv"">
                                                <h2><b>Sidelines</b></h2>
                                                [SROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together"" id=""LanguageDiv"">
                                                <h2><b>Languages</b></h2>
                                                [LROW]
                                            </div>
                                            <br />
                                            <div class=""keep-together"" id=""SkillDiv"" style=""margin-bottom:-20px!important;"">
                                                <h2><b>Skills</b></h2>
                                                <div class=""SkillRow"">
                                                <span class=""answer"">[SKILLNAME]</span>
                                                </div>
                                            </div>
                                            <br />
                                            <div class=""keep-together"" id=""HobbyDiv"" style=""margin-bottom:-20px!important;"">
                                                <h2><b>Hobbies</b></h2>
                                                <div class=""HobbyRow"">
                                                <span class=""answer"">[HOBBYNAME]</span>
                                                </div>
                                            </div>
                                            <br />
                                            <div class=""keep-together"" id=""CompetenceDiv"" style=""margin-bottom:-20px!important;"">
                                                <h2><b>Competences</b></h2>
                                                <div class=""CompetenceRow"">
                                                <span class=""answer"">[COMPETENCENAME]</span>
                                                </div>
                                            </div>
                                            <br />
                                            <div class=""keep-together""  id=""ReferenceDiv"" style=""margin-bottom:-20px!important;"">
                                                  <h2><b>References</b></h2>
                                                  [RROW]
                                            </div>
                                        </body>
                                    </html>";
            return template;
        }
    }
}