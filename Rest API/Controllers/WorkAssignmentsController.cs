using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetRestApi.Models;


namespace TimesheetRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkAssignmentsController : ControllerBase
    {

        //------------------Haetaan kaikki aktiiviset työtehtävät-----------------------------------

        [HttpGet]
        [Route("")]
        public string[] GetAll()
        {
            string[] assignmentNames = null;
            tuntidbContext context = new tuntidbContext();

            assignmentNames = (from wa in context.WorkAssignments
                               where (wa.Active == true && wa.Completed == false)  // Muutettu 4.5.
                               select wa.Title).ToArray();

            return assignmentNames;
        }

        //------POST-------START-ja-STOP operaatioiden hallinta------palauttaa booleanin mobiiliin----mutta-tekee-tietokantaan-muutoksia--------

        [HttpPost]
        public bool PostStatus(WorkAssignmentOperationModel input) // Todo: Ota myös customer ja employee tieto
        {
            tuntidbContext context = new tuntidbContext();

            try
            {       //Ensin haetaan työtehtävä Otsikon (title) perusteella ja nimetään se assignment muuttujaksi

                WorkAssignments assignment = (from wa in context.WorkAssignments
                                              where (wa.Active == true) &&
                                              (wa.Title == input.AssignmentTitle)
                                              select wa).FirstOrDefault();

                if (assignment == null)
                {
                    return false;
                }

                //--------------------------IF--START----------------------------------------------------------------------

                else if (input.Operation == "Start")
                {
                    if (assignment.InProgress == true || assignment.Completed == true) // Lisätty 4.5.

                    {
                        return false;
                    }
                    else
                    {


                        int assignmentId = assignment.IdWorkAssignment;

                        // Luodaan uusi timesheet työlle

                        Timesheet newEntry = new Timesheet()
                        {
                            IdWorkAssignment = assignmentId,
                            StartTime = DateTime.Now,
                            //WorkComplete = false, ------------ poistettu turha kenttä
                            Active = true,
                            CreatedAt = DateTime.Now
                        };

                        context.Timesheet.Add(newEntry);

                        // Päivitetään työtehtävän tilaa myös work assignments tauluun

                        WorkAssignments assignments = (from wa in context.WorkAssignments
                                                       where (wa.IdWorkAssignment == assignmentId) &&
                                                       (wa.Active == true)
                                                       select wa).FirstOrDefault();

                        assignment.InProgress = true;
                        assignment.WorkStartedAt = DateTime.Now;
                        assignment.LastModifiedAt = DateTime.Now;

                        context.SaveChanges(); // talletetaan kaikki em muutokset
                    }
                }

                //--------------------------------IF--STOP----------------------------------------------------------------------

                else if (input.Operation == "Stop")
                {
                    int assignmentId = assignment.IdWorkAssignment;

                    // Halutaan muuttaa tietoja sekä timesheetiin...

                    Timesheet existing = (from ts in context.Timesheet
                                          where (ts.IdWorkAssignment == assignmentId) &&
                                          (ts.Active == true)
                                          select ts).FirstOrDefault();

                    //............että work assignmentteihin

                    WorkAssignments assignments = (from wa in context.WorkAssignments
                                                   where (wa.IdWorkAssignment == assignmentId) &&
                                                   (wa.Active == true)
                                                   select wa).FirstOrDefault();

                    if (existing != null && assignment != null)
                    {

                        if (assignment.InProgress == false || assignment.Completed == true) // Muutettu 4.5.

                        {
                            return false;
                        }
                        else
                        {


                            // Timesheetin uudet arvot

                            existing.StopTime = DateTime.Now;
                            existing.WorkComplete = true;
                            existing.LastModifiedAt = DateTime.Now;


                            // Assignmentin uudet arvot

                            assignment.LastModifiedAt = DateTime.Now;
                            assignment.Completed = true;
                            assignment.CompletedAt = DateTime.Now;
                            assignment.InProgress = false;
                        }
                    }

                    else
                    {
                        return false; // Jos id tieto on null jommassa kummassa. 
                    }

                }

                    context.SaveChanges(); // talletetaan kaikki em muutokset
            }

            catch
            {
                return false; // Jos jotain muuta menee pileen.
            }

            finally
            {
                context.Dispose();
            }
            return true; // Mobiilisovellukselle palautetaan true
        }
    } 
}
