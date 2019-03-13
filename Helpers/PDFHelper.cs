using Codaxy.WkHtmlToPdf;
using ResumeStripper.Models;
using ResumeStripper.Models.Experiences;
using System;
using System.Web;

namespace ResumeStripper.Helpers
{
    public class PDFHelper
    {
        bool hasEducation = false;
        bool hasWork = false;
        bool hasCourse = false;
        bool hasSideline = false;
        bool hasLanguage = false;
        bool hasSkill = false;
        bool hasHobby = false;
        bool hasReference = false;
        bool hasCompetence = false;

        const string EducationPiece =
            @"<div class=""EduRow"">
            <p>Name Education: [EDUCATIONNAME]</p>
            <p>Level of Education: [EDUCATIONLEVEL]</p>
            <p>Name Institute: [INSTITUTENAME]</p>
            <p>Location Institute: [INSTITUTELOCATION]</p>
            <p>Begin Date: [EDUBEGIN]</p>
            <p>End Date: [EDUEND]</p>
            <p>Diploma: [EDUDIPLOMA]</p>
            </div>
            <br />
            [EROW]";
        const string WorkPiece =
            @"<div class=""WorkRow"">
            <p>Job Title: [WORKJOB]</p>
            <p>Name Company: [COMPANYNAME]</p>
            <p>Location Company: [COMPANYLOCATION]</p>
            <p>Task Description: <br />[WORKDESCRIPTION]</p>
            <p>Begin Date: [WORKBEGIN]</p>
            <p>End Date: [WORKEND]</p>
            </div>
            <br />
            [WROW]";
        const string CoursePiece =
            @"<div class=""CourseRow"">
            <p>Name Course: [COURSENAME]</p>
            <p>Name Institute: [COURSEINSTITUTENAME]</p>
            <p>Location Institute: [COURSEINSTITUTELOCATION]</p>
            <p>Year: [COURSEYEAR]</p>
            <p>Diploma: [COURSECERTIFICATE]</p>
            </div>
            <br />
            [CROW]";
        const string SidelinePiece =
            @"<div class=""SidelineRow"">
            <p>Job Title: [SIDELINEJOB]</p>
            <p>Name Company: [ORGANIZATIONNAME]</p>
            <p>Location Company: [ORGANIZATIONLOCATION]</p>
            <p>Task Description: <br />[SIDELINEDESCRIPTION]</p>
            <p>Begin Date: [SIDELINEBEGIN]</p>
            <p>End Date: [SIDELINEEND]</p>
            </div>
            <br />
            [SROW]";
        const string LanguagePiece =
            @"<div class=""LanguageRow"">
            <p>Name: [LANGUAGENAME]</p>
            <p>Level: [LANGLEVEL]</p>
            <p>Level of Speaking: [LANGSPEAK]</p>
            <p>Level of Listening: [LANGLISTEN]</p>
            <p>Level of Writing: [LANGWRITE]</p>
            </div>
            <br />
            [LROW]";
        const string SkillPiece =
            @"<div class=""SkillRow"">
            <p>Name: [SKILLNAME]</p>
            </div>
            <br />
            [SKROW]";
        const string HobbyPiece =
            @"<div class=""HobbyRow"">
            <p>Name: [HOBBYNAME]</p>
            </div>
            <br />
            [HROW]";
        const string CompetencePiece =
            @"<div class=""CompetenceRow"">
            <p>Name: [COMPETENCENAME]</p>
            </div>
            <br />
            [COROW]";
        const string ReferencePiece =
            @"<div class=""ReferenceRow"">
            <p>Name: [REFERENCENAME]</p>
            <p>Company Name: [REFERENCECOMPANYNAME]</p>
            <p>Job Title: [REFERENCEJOBTITLE]</p>
            <p>Emailaddress: [REFERENCEEMAIL]</p>
            <p>Phone Number: [REFERENCEPHONE]</p>
            </div>
            <br />
            [RROW]";


        public void GetPDF(string url, CV cv)
        {
            string headerPath = HttpContext.Current.Server.MapPath("~/Content/img/Header.png");
            string footerPath = HttpContext.Current.Server.MapPath("~/Content/img/footer.png");

            string template = getTemplate(headerPath, footerPath);

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

            foreach (Language e in cv.Languages)
            {
                hasLanguage = true;
                //add piece to template
                template = template.Replace("[LROW]", LanguagePiece);

                //replace all parts with CV information
                template = template.Replace("[LANGUAGENAME]", e.Name);
                template = template.Replace("[LANGLEVEL]", e.Level.ToString());
                template = template.Replace("[LANGSPEAK]", e.LevelOfSpeaking.ToString());
                template = template.Replace("[LANGLISTEN]", e.LevelOfListening.ToString());
                template = template.Replace("[LANGWRITE]", e.LevelOfWriting.ToString());
            }

            foreach (Skill e in cv.Skills)
            {
                hasSkill = true;
                //add piece to template
                template = template.Replace("[SKROW]", SkillPiece);

                //replace all parts with CV information
                template = template.Replace("[SKILLNAME]", e.Name);
            }

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

            foreach (Hobby e in cv.Hobbies)
            {
                hasHobby = true;
                //add piece to template
                template = template.Replace("[HROW]", HobbyPiece);

                //replace all parts with CV information
                template = template.Replace("[HOBBYNAME]", e.Name);
            }

            foreach (Competence e in cv.Competences)
            {
                hasCompetence = true;
                //add piece to template
                template = template.Replace("[COROW]", CompetencePiece);

                //replace all parts with CV information
                template = template.Replace("[COMPETENCENAME]", e.Name);
            }

            if (!cv.IsAnonymous)
            {
                //not anonymous CV
                if (cv.Name != null && cv.Name != "")
                {
                    template = template.Replace("[FIRST]", cv.Name);
                }
                else
                {
                    template = template.Replace(@"<p>First Name: [FIRST]</p>", "");
                }

                if (cv.Prefix != null && cv.Prefix != "")
                {
                    template = template.Replace("[PRE]", cv.Prefix);
                }
                else
                {
                    template = template.Replace(@"<p>Prefix: [PRE]</p>", "");
                }

                if (cv.Surname != null && cv.Surname != "")
                {
                    template = template.Replace("[LAST]", cv.Surname);
                }
                else
                {
                    template = template.Replace(@"<p>Last Name: [LAST]</p>", "");
                }
            } else
            {
                //anonymous CV, hide names
                template = template.Replace("CV [FIRST] [PRE] [LAST]", "CV");
                template = template.Replace(@"<p>First Name: [FIRST]</p>", "");
                template = template.Replace(@"<p>Prefix: [PRE]</p>", "");
                template = template.Replace(@"<p>Last Name: [LAST]</p>", "");
            }

            if (cv.Residence != null && cv.Residence != "")
            {
                template = template.Replace("[RESIDENCE]", cv.Residence);
            }
            else
            {
                template = template.Replace(@"<p>Residence: [RESIDENCE]</p>", "");
            }

            if (cv.Country != null && cv.Country != "")
            {
                template = template.Replace("[COUNTRY]", cv.Country);
            }
            else
            {
                template = template.Replace(@"<p>Country: [COUNTRY]</p>", "");
            }

            if (cv.DateOfBirth != null && cv.DateOfBirth != DateTime.MinValue)
            {
                template = template.Replace("[DOB]", cv.DateOfBirth.Date.ToShortDateString());
            }
            else //if date equals 1/1/0001 00:00:00 AM. aka if there was no dob entered
            {
                template = template.Replace(@"<p>Date of Birth: [DOB]</p>", "");
            }

            string licenses = "";

            if (cv.Licenses.Count != 0)
            {
                //adds all licenses to 1 string
                foreach (License l in cv.Licenses)
                {
                    licenses += l.Type.ToString() + ", ";
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

            if (cv.Profile != "" && cv.Profile != null)
            {
                template = template.Replace("[PROFILE]", cv.Profile);
            }
            else
            {
                template = template.Replace(@"<br />
                                    <p>Profile: <br />
                                        [PROFILE]
                                    </p>", "");
            }

            //removes fields based on if elements exist or not
            if (!hasEducation)
            {
                template = template.Replace(@"<h2><u>Educations</u></h2>", "");
            }

            if (!hasWork)
            {
                template = template.Replace(@"<h2><u>Work Experiences</u></h2>", "");
            }

            if (!hasCourse)
            {
                template = template.Replace(@"<h2><u>Courses</u></h2>", "");
            }

            if (!hasSideline)
            {
                template = template.Replace(@"<h2><u>Sidelines</u></h2>", "");
            }

            if (!hasLanguage)
            {
                template = template.Replace(@"<h2><u>Languages</u></h2>", "");
            }

            if (!hasSkill)
            {
                template = template.Replace(@"<h2><u>Skills</u></h2>", "");
            }

            if (!hasHobby)
            {
                template = template.Replace(@"<h2><u>Hobbies</u></h2>", "");
            }

            if (!hasCompetence)
            {
                template = template.Replace(@"<h2><u>Competences</u></h2>", "");
            }

            if (!hasReference)
            {
                template = template.Replace(@"<h2><u>References</u></h2>", "");
            }

            template = template.Replace("[EROW]", "");
            template = template.Replace("[WROW]", "");
            template = template.Replace("[CROW]", "");
            template = template.Replace("[SROW]", "");
            template = template.Replace("[LROW]", "");
            template = template.Replace("[SKROW]", "");
            template = template.Replace("[HROW]", "");
            template = template.Replace("[RROW]", "");
            template = template.Replace("[COROW]", "");

            if (!hasLanguage) template = template.Replace(@"<h2>Languages</h2>", "");

            if (!hasSkill) template = template.Replace(@"<h2>Skills</h2>", "");

            if (!hasHobby) template = template.Replace(@"<h2>Hobbies</h2>", "");

            if (!hasReference) template = template.Replace(@"<h2>References</h2>", "");

            if (!hasCompetence) template = template.Replace(@"<h2>Competences</h2>", "");

            //just cleanup of empty paragraphs if any
            template = template.Replace("<p></p>", "");
            template = template.Replace("<p> </p>", "");

            string headUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Header.html");
            string footUrl = HttpContext.Current.Server.MapPath("~/Views/Shared/Footer.html");

            PdfConvert.ConvertHtmlToPdf(new Codaxy.WkHtmlToPdf.PdfDocument
            {
                Html = template,
                HeaderUrl = headUrl,
                FooterUrl = footUrl
                //HeaderRight = "Page [page] of [topage]"
            },
            new PdfOutput
            {
                OutputFilePath = url
            });
        }

        public string getImgHtml(string path)
        {
            string htmlImgBegin = @"<img style=""width=100%;height:100%;object-fit:cover;"" alt=""HEADERLOGO"" src=""";
            string htmlImgEnd = @"""/>";
            return htmlImgBegin + path + htmlImgEnd;
        }

        public string getTemplate(string headerUrl, string footerUrl)
        {
            Base64Converter converter = new Base64Converter();
            string header64 = converter.ImageToBase64(headerUrl);
            string footer64 = converter.ImageToBase64(footerUrl);

            string template = @"<html>
                                <style>html {color:OrangeRed;} body{margin-top:150px;margin-bottom:50px;} h2 {font-size: 32px;} p {font-size: 21px;} h1 {font-size: 40px;} #footer{position:fixed;bottom:0;}.keep-together {page-break-inside: avoid;}.break-before {page-break-before: always;}.break-after {page-break-after: always;}</style>
                                <head>
                                </head>
                                    <body>
                                    <h1>CV [FIRST] [PRE] [LAST]</h1>
                                    <br />
                                    <br />
                                    <div class=""keep-together"">
                                    <h2><u>Personalia</u></h2>
                                    <p>First Name: [FIRST]</p>
                                    <p>Prefix: [PRE]</p>
                                    <p>Last Name: [LAST]</p>
                                    <p>Residence: [RESIDENCE]</p>
                                    <p>Country: [COUNTRY]</p>
                                    <p>Date of Birth: [DOB]</p>
                                    <p>License: [LICENSE]</p>
                                    <br />
                                    <p>Profile: <br />
                                        [PROFILE]
                                    </p>
                                    </div>
                                    <br />
                                    <div class=""keep-together"" id=""EducationDiv"">
                                        <h2><u>Educations</u></h2>
                                        [EROW]
                                    </div >
                                    <br />
                                    <div id=""WorkDiv"">
                                        <h2><u>Work Experiences</u></h2>
                                        [WROW]
                                    </div>
                                    <br />
                                    <div class=""keep-together""  id=""CourseDiv"">
                                        <h2><u>Courses</u></h2>
                                        [CROW]
                                    </div>
                                    <br />
                                    <div class=""keep-together""  id=""SidelineDiv"">
                                        <h2><u>Sidelines</u></h2>
                                        [SROW]
                                    </div>
                                    <br />
                                    <div class=""keep-together""  id=""LanguageDiv"">
                                        <h2><u>Languages</u></h2>
                                        [LROW]
                                    </div>
                                    <br />
                                    <div class=""keep-together""  id=""SkillDiv"">
                                         <h2><u>Skills</u></h2>
                                         [SKROW]
                                    </div>
                                    <br />
                                    <div class=""keep-together""  id=""HobbyDiv"">
                                          <h2><u>Hobbies</u></h2>
                                         [HROW]
                                    </div>
                                    <br />
                                    <div class=""keep-together""  id=""CompetenceDiv"">
                                          <h2><u>Competences</u></h2>
                                         [COROW]
                                    </div>
                                    <br />
                                    <div class=""keep-together""  id=""ReferenceDiv"">
                                          <h2><u>References</u></h2>
                                         [RROW]
                                    </div>
                                    </body>
                                    </html>";
            return template;
        }
    }
}