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

        public string Skills = "";
        public string Licenses = "";
        public string Hobbies = "";
        public string Competences = "";

        public string Template = "";

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

        public byte[] GeneratePdf(CV cv)
        {
            //retrieves basic template
            GetTemplate();

            //capitalizes all relevant values in the different lists
            cv.CapitalizeLists();

            //fill the template with any lists in the cv
            SetUpEducation(cv);
            SetUpCourses(cv);
            SetUpWork(cv);
            SetUpSidelines(cv);
            SetUpLanguages(cv);
            SetUpSkills(cv);
            SetUpCompetences(cv);
            SetUpHobbies(cv);
            SetUpReferences(cv);

            //sets up the personalia section, be it anonymous or not
            SetUpPersonalia(cv);

            //sets up Profile
            SetUpProfile(cv);

            //cleans any unneeded or missing segments from the template
            CleanUpTemplate();

            string headUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Header.html");
            string footUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Footer.html");

            MemoryStream resultStream = new MemoryStream();

            PdfConvert.ConvertHtmlToPdf(new PdfDocument
            {
                Html = Template,
                HeaderUrl = headUrl,
                FooterUrl = footUrl,
            },
                new PdfOutput
                {
                    OutputStream = resultStream
                });

            return resultStream.ToArray();
        }

        private void GetTemplate()
        {
            Template = @"<html>
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
                                                        <span><b>Gender:</b></span>
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
                                                        <span class=""answer"">[GENDER]</span>
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
                                                    <h3 style=""margin-top:180px!important""><b>Profile</b></h3>
                                                    <span class=""answer"">[PROFILE]</span>
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
        }

        private void SetUpEducation(CV cv)
        {
            //EDUCATIONS
            foreach (EducationExperience e in cv.Educations)
            {
                HasEducation = true;
                //add piece to template
                Template = Template.Replace("[EROW]", EducationPiece);

                //replace all parts with CV information
                Template = Template.Replace("[EDUCATIONNAME]", e.Name);
                Template = Template.Replace("[EDUCATIONLEVEL]", e.LevelOfEducation);
                Template = Template.Replace("[INSTITUTENAME]", e.OrganizationName);
                Template = Template.Replace("[INSTITUTELOCATION]", e.LocationOrganization);
                Template = Template.Replace("[EDUBEGIN]", e.BeginDate.Date.ToShortDateString());

                DateTime current = DateTime.Today;
                //changes date to 'present' if current day is given
                if (current.ToString("M/d/yyyy").Equals(e.EndDate.Date.ToString("M/d/yyyy")))
                {
                    //given date is today
                    Template = Template.Replace("[EDUEND]", "present");
                }
                else
                {
                    Template = Template.Replace("[EDUEND]", e.EndDate.Date.ToShortDateString());
                }

                string result = "";

                result = e.Diploma ? "Diploma acquired" : "Diploma not acquired";

                Template = Template.Replace("[EDUDIPLOMA]", result);
            }
        }

        private void SetUpCourses(CV cv)
        {
            //COURSES
            foreach (CourseExperience e in cv.Courses)
            {
                HasCourse = true;
                //add piece to template
                Template = Template.Replace("[CROW]", CoursePiece);

                //replace all parts with CV information
                Template = Template.Replace("[COURSENAME]", e.Name);
                Template = Template.Replace("[COURSEINSTITUTENAME]", e.OrganizationName);
                Template = Template.Replace("[COURSEINSTITUTELOCATION]", e.LocationOrganization);
                Template = Template.Replace("[COURSEYEAR]", e.Year.Date.Year.ToString());

                string result = "";

                result = e.Certificate ? "Certificate acquired" : "Certificate not acquired";

                Template = Template.Replace("[COURSECERTIFICATE]", result);
            }
        }

        private void SetUpWork(CV cv)
        {
            //WORK EXPERIENCES
            foreach (WorkExperience e in cv.WorkExperiences)
            {
                HasWork = true;
                //add piece to template
                Template = Template.Replace("[WROW]", WorkPiece);

                //replace all parts with CV information
                Template = Template.Replace("[WORKJOB]", e.JobTitle);
                Template = Template.Replace("[COMPANYNAME]", e.OrganizationName);
                Template = Template.Replace("[COMPANYLOCATION]", e.LocationOrganization);
                Template = Template.Replace("[WORKDESCRIPTION]", e.TaskDescription);
                Template = Template.Replace("[WORKBEGIN]", e.BeginDate.Date.ToShortDateString());

                DateTime current = DateTime.Today;
                //changes date to 'present' if current day is given
                if (current.ToString("M/d/yyyy").Equals(e.EndDate.Date.ToString("M/d/yyyy")))
                {
                    //given date is today
                    Template = Template.Replace("[WORKEND]", "present");
                }
                else
                {
                    Template = Template.Replace("[WORKEND]", e.EndDate.Date.ToShortDateString());
                }
            }
        }

        private void SetUpSidelines(CV cv)
        {
            //SIDELINES
            foreach (SidelineExperience e in cv.SideLines)
            {
                HasSideline = true;
                //add piece to template
                Template = Template.Replace("[SROW]", SidelinePiece);

                //replace all parts with CV information
                Template = Template.Replace("[SIDELINEJOB]", e.JobTitle);
                Template = Template.Replace("[ORGANIZATIONNAME]", e.OrganizationName);
                Template = Template.Replace("[ORGANIZATIONLOCATION]", e.LocationOrganization);
                Template = Template.Replace("[SIDELINEDESCRIPTION]", e.TaskDescription);
                Template = Template.Replace("[SIDELINEBEGIN]", e.BeginDate.Date.ToShortDateString());

                DateTime current = DateTime.Today;
                //changes date to 'present' if current day is given
                if (current.ToString("M/d/yyyy").Equals(e.EndDate.Date.ToString("M/d/yyyy")))
                {
                    //given date is today
                    Template = Template.Replace("[SIDELINEEND]", "present");
                }
                else
                {
                    Template = Template.Replace("[SIDELINEEND]", e.EndDate.Date.ToShortDateString());
                }
            }
        }

        private void SetUpLanguages(CV cv)
        {
            //LANGUAGES
            foreach (Language e in cv.Languages)
            {
                HasLanguage = true;

                //capitalizes first letter
                e.Name = cv.CapitalizeFirstLetter(e.Name);

                if (e.IsSimple)
                {
                    string type = e.Level.ToString();
                    //simple mode used, only display Level

                    //add piece to template
                    Template = Template.Replace("[LROW]", LanguagePieceSimple);

                    if (type.Equals("VeryGood"))
                    {
                        type = "Very Good";
                    }

                    Template = Template.Replace("[LANGLEVEL]", type);
                }
                else
                {
                    //detailed mode used, only display separate levels
                    string typeSpeak = e.LevelOfSpeaking.ToString();
                    string typeWrite = e.LevelOfWriting.ToString();
                    string typeListen = e.LevelOfListening.ToString();

                    //add piece to template
                    Template = Template.Replace("[LROW]", LanguagePieceDetailed);

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

                    Template = Template.Replace("[LANGSPEAK]", typeSpeak);
                    Template = Template.Replace("[LANGLISTEN]", typeListen);
                    Template = Template.Replace("[LANGWRITE]", typeWrite);
                }

                //replace language name
                Template = Template.Replace("[LANGNAME]", e.Name);
            }
        }

        private void SetUpSkills(CV cv)
        {
            //SKILLS
            foreach (Skill e in cv.Skills)
            {
                HasSkill = true;

                //capitalizes first letter
                e.Name = cv.CapitalizeFirstLetter(e.Name);

                Skills += e.Name + ", ";
            }

            //removes spare ', '
            Skills = Skills.TrimEnd(' ');
            Skills = Skills.TrimEnd(',');

            Template = Template.Replace("[SKILLNAME]", Skills);
        }

        private void SetUpHobbies(CV cv)
        {
            //HOBBIES
            foreach (Hobby e in cv.Hobbies)
            {
                HasHobby = true;

                //capitalizes first letter
                e.Name = cv.CapitalizeFirstLetter(e.Name);

                Hobbies += e.Name + ", ";
            }

            //removes spare ', '
            Hobbies = Hobbies.TrimEnd(' ');
            Hobbies = Hobbies.TrimEnd(',');

            Template = Template.Replace("[HOBBYNAME]", Hobbies);
        }

        private void SetUpCompetences(CV cv)
        {
            //COMPETENCES
            foreach (Competence e in cv.Competences)
            {
                HasCompetence = true;

                //capitalizes first letter
                e.Name = cv.CapitalizeFirstLetter(e.Name);

                Competences += e.Name + ", ";
            }

            //removes spare ', '
            Competences = Competences.TrimEnd(' ');
            Competences = Competences.TrimEnd(',');

            Template = Template.Replace("[COMPETENCENAME]", Competences);
        }

        private void SetUpReferences(CV cv)
        {
            //REFERENCES
            foreach (Reference e in cv.References)
            {
                HasReference = true;
                //add piece to template
                Template = Template.Replace("[RROW]", ReferencePiece);

                //replace all parts with CV information
                Template = Template.Replace("[REFERENCENAME]", e.Name);
                Template = Template.Replace("[REFERENCECOMPANYNAME]", e.CompanyName);
                Template = Template.Replace("[REFERENCEJOBTITLE]", e.JobTitle);
                Template = Template.Replace("[REFERENCEEMAIL]", e.Email);
                Template = Template.Replace("[REFERENCEPHONE]", e.PhoneNumber);
            }
        }

        private void SetUpPersonalia(CV cv)
        {
            if (cv.IsAnonymous)
            {
                //CV must be anonymous
                SetUpAnonymousName(cv);
            }
            else
            {
                //CV must NOT be anonymous
                SetUpName(cv);
            }

            //Gender can't be null or empty anyway
            Template = Template.Replace(@"[GENDER]", cv.Gender.ToString());

            //TODO: these can be moved if anonymous variants are created maybe
            SetUpResidence(cv);
            SetUpCountry(cv);
            SetUpDateOfBirth(cv);
            SetUpLicenses(cv);
        }

        private void SetUpName(CV cv)
        {
            if (!string.IsNullOrEmpty(cv.Name))
            {
                //capitalizes first letter
                cv.Name = cv.CapitalizeFirstLetter(cv.Name);

                Template = Template.Replace("[FIRST]", cv.Name);
            }
            else
            {
                Template = Template.Replace(@"<span><b>First Name:</b></span>
                                                    <br />", "");
                Template = Template.Replace(@"<span class=""answer"">[FIRST]</span>
                                                    <br />", "");
            }

            if (!string.IsNullOrEmpty(cv.Prefix))
            {
                Template = Template.Replace("[PRE]", cv.Prefix);
            }
            else
            {
                Template = Template.Replace(@"
                                                        <span><b>Prefix:</b></span>
                                                        <br />", "");
                Template = Template.Replace(@"
                                                        <span class=""answer"">[PRE]</span>
                                                        <br />", "");
                Template = Template.Replace(@" [PRE] ", " ");
            }

            if (!string.IsNullOrEmpty(cv.Surname))
            {
                //capitalizes first letter
                cv.Surname = cv.CapitalizeFirstLetter(cv.Surname);

                Template = Template.Replace("[LAST]", cv.Surname);
            }
            else
            {
                Template = Template.Replace(@"<span><b>Last Name:</b></span>
                                                    <br />", "");
                Template = Template.Replace(@"<span class=""answer"">[LAST]</span>
                                                    <br />", "");
            }
        }

        private void SetUpAnonymousName(CV cv)
        {
            //TODO: Keep First Name
            //deletes anything to do with name
            Template = Template.Replace(@"[FIRST] [PRE] [LAST]", "Curriculum vitae");
            Template = Template.Replace(@"<h4 class=""CVTitle""><i>Curriculum vitae</i></h4>", "");
            Template = Template.Replace(@"<span><b>First Name:</b></span>
                                                    <br />", "");
            Template = Template.Replace(@"<span class=""answer"">[FIRST]</span>
                                                    <br />", "");
            Template = Template.Replace(@"<span><b>Prefix:</b></span>
                                                    <br />", "");
            Template = Template.Replace(@"<span class=""answer"">[PRE]</span>
                                                    <br />", "");
            Template = Template.Replace(@"<span><b>Last Name:</b></span>
                                                    <br />", "");
            Template = Template.Replace(@"<span class=""answer"">[LAST]</span>
                                                    <br />", "");
        }

        private void SetUpResidence(CV cv)
        {
            //RESIDENCE
            if (!string.IsNullOrEmpty(cv.Residence))
            {
                //capitalizes first letter
                cv.Residence = cv.CapitalizeFirstLetter(cv.Residence);

                Template = Template.Replace("[RESIDENCE]", cv.Residence);
            }
            else
            {
                Template = Template.Replace(@"<span><b>Residence:</b></span>
                                                    <br />", "");
                Template = Template.Replace(@"<span class=""answer"">[RESIDENCE]</span>
                                                    <br />", "");
            }
        }

        private void SetUpCountry(CV cv)
        {
            //COUNTRY
            if (!string.IsNullOrEmpty(cv.Country))
            {
                //capitalizes first letter
                cv.Country = cv.CapitalizeFirstLetter(cv.Country);

                Template = Template.Replace("[COUNTRY]", cv.Country);
            }
            else
            {
                Template = Template.Replace(@"<span><b>Country:</b></span>
                                                    <br />", "");
                Template = Template.Replace(@"<span class=""answer"">[COUNTRY]</span>
                                                    <br />", "");
            }
        }

        private void SetUpDateOfBirth(CV cv)
        {
            //DATE OF BIRTH
            if (cv.DateOfBirth != DateTime.MinValue)
            {
                Template = Template.Replace("[DOB]", cv.DateOfBirth.Date.ToShortDateString());
            }
            else //if date equals 1/1/0001 00:00:00 AM. aka if there was no dob entered
            {
                Template = Template.Replace(@"<span><b>Date of Birth:</b></span>
                                                        <br />", "");
                Template = Template.Replace(@"<span class=""answer"">[DOB]</span>
                                                        <br />", "");
            }
        }

        private void SetUpLicenses(CV cv)
        {
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

                Template = Template.Replace("[LICENSE]", Licenses);
            }
            else
            {
                //there are no licenses
                Template = Template.Replace("[LICENSE]", "None");
            }
        }

        private void SetUpProfile(CV cv)
        {
            //PROFILE
            if (!string.IsNullOrEmpty(cv.Profile))
            {
                //capitalizes first letter
                cv.Profile = cv.CapitalizeFirstLetter(cv.Profile);
                Template = Template.Replace("[PROFILE]", cv.Profile);
            }
            else
            {
                Template = Template.Replace(@"<div class=""keep-together"">
                                                    <h3 style=""margin-top:180px!important""><b>Profile</b></h3>
                                                    <span class=""answer"">[PROFILE]</span>
                                                </div>", "");
            }
        }

        private void CleanUpTemplate()
        {
            //removes fields based on if elements exist or not
            if (!HasEducation)
            {
                Template = Template.Replace(@"<h2><b>Educations</b></h2>", "");
            }

            if (!HasWork)
            {
                Template = Template.Replace(@"<h2><b>Work Experiences</b></h2>", "");
            }

            if (!HasCourse)
            {
                Template = Template.Replace(@"<h2><b>Courses</b></h2>", "");
            }

            if (!HasSideline)
            {
                Template = Template.Replace(@"<h2><b>Sidelines</b></h2>", "");
            }

            if (!HasLanguage)
            {
                Template = Template.Replace(@"<h2><b>Languages</b></h2>", "");
            }

            if (!HasSkill)
            {
                Template = Template.Replace(@"<h2><b>Skills</b></h2>", "");
            }

            if (!HasHobby)
            {
                Template = Template.Replace(@"<h2><b>Hobbies</b></h2>", "");
            }

            if (!HasCompetence)
            {
                Template = Template.Replace(@"<h2><b>Competences</b></h2>", "");
            }

            if (!HasReference)
            {
                Template = Template.Replace(@"<h2><b>References</b></h2>", "");
            }

            Template = Template.Replace("[EROW]", "");
            Template = Template.Replace("[WROW]", "");
            Template = Template.Replace("[CROW]", "");
            Template = Template.Replace("[SROW]", "");
            Template = Template.Replace("[LROW]", "");
            Template = Template.Replace("[RROW]", "");

            //just cleanup of empty paragraphs and spans if any
            Template = Template.Replace("<p></p>", "");
            Template = Template.Replace("<p> </p>", "");
            Template = Template.Replace("<p><b></b></p>", "");
            Template = Template.Replace("<p><b> </b></p>", "");
            Template = Template.Replace("<span></span>", "");
            Template = Template.Replace("<span> </span>", "");
            Template = Template.Replace("<span><b></b></span>", "");
            Template = Template.Replace("<span><b> </b></span>", "");
        }
    }
}