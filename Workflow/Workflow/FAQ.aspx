<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="Workflow.FAQ" Title="FAQ" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="shortcut icon" type="image/png" href="assets/icons/rit_insignia.png" />
    <script type="text/javascript" src="assets/js/jquery.js"></script>
    <script type="text/javascript" src="assets/js/semantic.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="assets/css/styles.css" type="text/css" />
    <link rel="stylesheet" href="assets/css/semantic.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Titillium+Web" rel="stylesheet" />

    <title>FAQ</title>
</head>
<body id="faq-background">
    <div id="faq-body">
        <div id="faq-header">
            <h1>Frequently Asked Questions</h1>
        </div>
        <div id="faq-general">
            <div class="ui accordion">
                <div class="title">
                    <i class="dropdown icon"></i>
                    General
                </div>
                <div class="content">
                    <div class="content">
                        <div class="ui styled accordion">
                            <div class="title">
                                <i class="dropdown icon"></i>
                                How do I get access?
                            </div>
                            <div class="content">
                                <p class="transition hidden">
                                    In order to access information on your project, you need to have an account created by a Venture Creations administrator. If you have
                    forgotten your password, go to the "Forgot Password" page available off of the Login page.
                                </p>
                            </div>
                            <div class="title">
                                <i class="dropdown icon"></i>
                                Why can I no longer access my account?
                            </div>
                            <div class="content">
                                <p class="transition visible">
                                    If you are having trouble logging in, your account may have been deleted. Company accounts may be deleted if the project attached to
                                them have been completed. If you think this is an error, please contact a Venture Creations administrator.
                                </p>
                            </div>
                            <div class="title">
                                <i class="dropdown icon"></i>
                                How do I reset my password?
                            </div>
                            <div class="content">
                                <p>
                                    On the login page you can find an option that says "Forgot Password" where you can enter your email and a reset link will be sent to that email.
                                    <i>If you cannot remember the email used or you are being told that the wrong email has been entered, 
                                        contact a Venture Creations coach or administrator for help.</i>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- *************************************************************-->

            <div class="ui accordion">
                <div class="title">
                    <i class="dropdown icon"></i>
                    Forms
                </div>
                <div class="content">
                    <div class="content">
                        <div class="ui styled accordion">
                            <div class="title">
                                <i class="dropdown icon"></i>
                                What's the difference between a form and a template?
                            </div>
                            <div class="content">
                                <p class="transition hidden">
                                    Think of a template as a document on your computer. In order to fill it out, you need to print it. You can print this document
                                    as many times as you want and add different information to each print out. So, a template is the unfilled version of a form.
                                    Templates can be assigned to a workflow, where it becomes a form that can be filled out.
                                    <i>Once a template has been assigned to a workflow, the layout of the template can no longer be changed.</i>
                                </p>
                                <p>
                                    Continuing with the comparison above, a form is a filled out version of the document on your computer. So, a form has project-specific
                                    information on it that the template doesn't have. Information put on the form can be adjusted if necessary.
                                </p>
                                <p>
                            </div>
                            <div class="title">
                                <i class="dropdown icon"></i>
                                Why can I not edit a form template?
                            </div>
                            <div class="content">
                                <p class="transition visible">
                                    Once a template has been assigned to a workflow, it can no longer be edited. However, a new form can be made.
                                </p>
                            </div>
                            <div class="title">
                                <i class="dropdown icon"></i>
                                Why can I not change information on my form?
                            </div>
                            <div class="content">
                                <p>
                                    If a form has been submitted for review, the information on it cannot be changed. If you made a mistake that needs to be changed,
                                    contact the Venture Creations coach who is set to review the form you submitted and they can deny the pending approval, which will allow
                                    you to make changes and resubmit.
                                </p>
                            </div>
                            <div class="title">
                                <i class="dropdown icon"></i>
                                What do all the fields in form template creation mean?
                            </div>
                            <div class="content">
                                <h4>Autocomplete</h4>
                                <h4>Checkbox Group</h4>
                                <p>A checkbox group allows for multiple options to be selected.</p>
                                <h4>Date Field</h4>
                                <p>A date field will only allow a date to be accepted as an answer.</p>
                                <h4>File Upload</h4>
                                <p>A file upload will allow the user to upload any necessary files and attach it to their form submission.</p>
                                <h4>Header</h4>
                                <p>A header is static information. This can be used for various purposes like signifying important information or to mark the start of a new section on a form.</p>
                                <h4>Number</h4>
                                <p>A number field will only allow for a number to be entered into it. This can be used for things like estimated earnings or hours spent on a project.</p>
                                <h4>Paragraph</h4>
                                <p>A paragraph is static content that cannot be edited by someone filling out the form. This can be used to put bulk text explanations or questions in.</p>
                                <h4>Radio Group</h4>
                                <p>A radio group is like a checkbox group, but it allows for only ONE selection from the list of options.</p>
                                <h4>Select</h4>
                                <p>A select is similar to a radio group but it shows as a dropdown list where you can specify if a user can select only one or more than one option.</p>
                                <h4>Text Field</h4>
                                <p>A text field is where a user can input text. A text field is good specifically for shorter answers; for longer answers, consider a text area.</p>
                                <h4>Text Area</h4>
                                <p>A text are is like a text field but the size of the input box can be adjusted if the user is entering a lot of text into the box. This would be good for questions that require longer answers.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- *************************************************************-->
            <div class="ui accordion">
                <div class="title">
                    <i class="dropdown icon"></i>
                    Workflows
                </div>
                <div class="content">
                    <div class="content">
                        <div class="ui styled accordion">
                            <div class="title">
                                <i class="dropdown icon"></i>
                                Why can I not make changes to a workflow?
                            </div>
                            <div class="content">
                                <p class="transition hidden">
                                    Workflows can only be edited when it has not been assigned to a project.
                                </p>
                            </div>
                            <div class="title">
                                <i class="dropdown icon"></i>
                                What is a workflow? How is it different from a project?
                            </div>
                            <div class="content">
                                <p class="transition visible">
                                    Think of a workflow as the barebones skeleton of a project; a workflow is the specific step-by-step process that a project needs to follow.     
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- *************************************************************-->
            <div class="ui accordion">
                <div class="title">
                    <i class="dropdown icon"></i>
                    Projects
                </div>
                <div class="content">
                    <div class="content">
                        <div class="ui styled accordion">
                            <div class="title">
                                <i class="dropdown icon"></i>
                                I am trying to make a new project but I cannot find the right company in the list?
                            </div>
                            <div class="content">
                                <p class="transition hidden">
                                    If the company you are trying to find does not appear in the dropdown list of project, this most likely means
                                    that the company has not been added to the system yet. You, or an administrator, will need to use the Admin Panel
                                    to add this company to the list.
                                </p>
                            </div>
                            <div class="title">
                                <i class="dropdown icon"></i>
                                Information on my project has changed; how do I change it?
                            </div>
                            <div class="content">
                                <p>
                                    In order to edit a project's information (funding source, founder name, etc.) you will need to navigate to the Admin Panel.
                                    This can be found in the dropdown at the top right of the screen. From there you can change the company's name under the Company tab
                                    or other project information can be found under the Project tab.
                                </p>
                                <p>
                                    <i>If you do not have permission to edit information on your project, reach out to a Venture Creations coach and they can
                                    assist you.
                                    </i>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(".ui.accordion").accordion();
        $(".ui.styled.accordion").accordion('open');
    </script>
</body>
</html>
