using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsFramework
{
    public class Program
    {
        [TestFixture]
        [Category("Sprint1")]
        class Tenant : Global.Base
        {

            [Test]
            public void UserAccount()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Edit the Profile");

                // Create an object of Profile Class to call the method
                Profile obj = new Profile();
                obj.EditProfile();

            }
        }

        [TestFixture]
        [Category("Sprint2")]
        class User : Global.Base
        {
            /**TC_001_02 Check if the user is able to Share a Service and displays on 
              "Manage Listing" page. BUG-1- Show Error Message while sharing skill with worksamples 
              and the service is listed without worksamples which is not expected.
              BUG-2- While sharing skill with servicetype- "Hourly basis service", the service is
              displayed correctly in manage listing page but when we view the service in detail,
              it displayes "One-Off" instead of "Hourly"****/

            [Test]
            public void SS_ShareSkill()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Share a Service");

                // Create an object of ShareSkill Class to call the method
                ShareSkill obj = new ShareSkill();
                obj.ShareASkill("create");

            }


            /*** TC_001_04 Check if the user is able to cancel sharing a service after
              adding all the user inputs.****/
            [Test]
            public void SS_ShareSkillWithCancel()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Cancel the form of ShareSkill");

                // Create an object of ShareSkill Class to call the method
                ShareSkill obj = new ShareSkill();
                obj.ShareASkill("cancel");
            }


            /*** TC_001_10 Check if the user is able to upload more than 5 worksamples 
              while sharing a skill. BuG - Error message should display after uploading 5 
              worksamples.Due to Failing the same, unable to verify the testcase****/
            [Test]
            public void SS_ShareSkillWithMoreImage()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Share a Service with more worksamples");

                // Create an object of ShareSkill Class to call the method
                ShareSkill obj = new ShareSkill();
                obj.ShareASkill("moresample");

            }

            /*** TC_001_07 Check if the user is able to add special characters 
             in "Title" field****/
            [Test]
            public void SS_ShareSkillWithSpChar()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Enter SPChar on Title with ShareSkill");

                // Create an object of ShareSkill Class to call the method
                ShareSkill obj = new ShareSkill();
                obj.ShareASkill("spchar");
            }

            /*** TC_001_05 Check if the user is able to ReShare an existing  Skill 
             on "Manage Listing" page. BUG - Resharing is possible, failing in restricting 
             on resharing the same skill ****/
            [Test]
            public void SS_ShareSkillReshare()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Reshare a service");

                // Create an object of ShareSkill Class to call the method
                ShareSkill obj = new ShareSkill();
                obj.ShareASkill("create");
                obj.ShareASkill("create");
            }

            /*** TC_002_03 Check if the user is able to edit the service details listed 
             on "Manage Listings" page and make sure the changes are reflected.
             BuG - User should be able to edit one Field alone instead of force editing all the fields
             and User should be able to see the previous details while editing.Main Functionalty is broken****/
            [Test]
            public void SS_EditAService()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Edit A Service");

                // Create an object of ManageListings Class to call the method
                ManageListings obj = new ManageListings();
                obj.EditService();
            }

            /*** TC_002_05 Check if the user is able to delete a service by clicking "Yes" 
            on Confirmation Message and ensure the service is not listed on Search results****/
            [Test]
            public void SS_DeleteService()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Delete A Service");

                // Create an object of ManageListings Class to call the method
                ManageListings obj = new ManageListings();
                obj.DeleteService();
            }

        }
        

     }

}
 