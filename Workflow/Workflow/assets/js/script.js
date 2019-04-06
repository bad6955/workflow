/**
 * ******* ADMIN JS
 */
function AdminPageLoaded() {
    $('.menu .item')
        .tab()
        ;
    $('.ui.dropdown')
        .dropdown()
        ;
}

function saveSelection() {
    var roleEle = document.getElementById("RoleSelect");
    var role = roleEle.options[roleEle.selectedIndex].value;

    var companyEle = document.getElementById("CompanySelect");
    var company = companyEle.options[companyEle.selectedIndex].value;

    var accountEle = document.getElementById("LockedAccountSelect");
    var account = accountEle.options[accountEle.selectedIndex].value;

    document.getElementById("SelectedRole").value = role;
    document.getElementById("SelectedCompany").value = company;
    document.getElementById("SelectedAccount").value = account;

    /* if selected role is not a Client, default company to VC */
    if (role !== 1) {
        document.getElementById("CompanySelect").value = 1;
    }
}
function DisplayDeleteUser() {
    $('.ui.tiny.delete.modal').modal('show');
}

function EditUser(fname, lname, rolename, roleid, userid) {
    $('.ui.small.test.modal').modal('show');

    document.getElementById("user_firstname").value = fname;
    document.getElementById("user_lastname").value = lname;
    document.getElementById("UserRole").value = roleid;
    document.getElementById("UserID").value = userid;
    document.getElementById("UserSelectedRole").value = roleid;
}

function saveUser() {
    var roleEle = document.getElementById("UserRole");
    var role = roleEle.options[roleEle.selectedIndex].value;

    document.getElementById("UserSelectedRole").value = role;
    document.getElementById("UserFirstName").value = document.getElementById("user_firstname").value;
    document.getElementById("UserLastName").value = document.getElementById("user_lastname").value;
}
/**
 * ******* FORMS JS
 */
function FormsPageLoaded() {
    $('.ui.dropdown')
        .dropdown();
    $('.menu .item')
        .tab();
    var viewerOptions = {
        dataType: 'json',
        formData: document.getElementById("formViewerData").value
    };
    var formViewer = $('#renderWrap').formRender(viewerOptions);
    document.getElementById("formViewerData").value = formViewer.formData;

    var builderOptions = {
        dataType: 'json',
        disabledActionButtons: ['data', 'save'],
        controlPosition: 'left',
        formData: document.getElementById("formBuilderData").value
    };
    var formBuilder = $('#buildWrap').formBuilder(builderOptions);
    document.getElementById("formBuilderData").value = formBuilder.formData;
    console.log("FormData: " + formBuilder.formData);
}
function SaveFormViewer() {
    SaveUploadedFiles();
    document.getElementById("formViewerData").value = JSON.stringify(formViewer.userData);
}
function SaveUploadedFiles() {
    var file = $("input:file")[0].files[0];
    var inputName = $("input:file")[0].name;
    if (file) {
        document.getElementById("fileUploadName").value = file.name;
        document.getElementById("fileInputName").value = inputName;
    }
}
function SubmitForm() {
    SaveFormViewer();
    jQuery(function () {
        formRenderOpts = {
            dataType: 'json',
            formData: formViewer.formData
        };
        var renderedForm = $('<div>');
        renderedForm.formRender(formRenderOpts);
        console.log(renderedForm.html());
        document.getElementById("formViewerData").value = renderedForm.html();
    });
}
function ApproveForm() {
    jQuery(function () {
        formRenderOpts = {
            dataType: 'json',
            formData: formViewer.formData
        };
        var renderedForm = $('#renderWrap');
        renderedForm.formRender(formRenderOpts);
        console.log(renderedForm.html());
        document.getElementById("formViewerData").value = renderedForm.html();
    });
}
function saveSelection() {
    console.log("Saving selectors");
    var app1 = document.getElementById("FormApproval1");
    var role1 = app1.options[app1.selectedIndex].value;
    document.getElementById("SelectedApprover1").value = role1;
    var app2 = document.getElementById("FormApproval2");
    if (app2 !== null) {
        var role2 = app2.options[app2.selectedIndex].value;
        document.getElementById("SelectedApprover2").value = role2;
    }
    var app3 = document.getElementById("FormApproval3");
    if (app3 !== null) {
        var role3 = app3.options[app3.selectedIndex].value;
        document.getElementById("SelectedApprover3").value = role3;
    }
    var app4 = document.getElementById("FormApproval4");
    if (app4 !== null) {
        var role4 = app4.options[app4.selectedIndex].value;
        document.getElementById("SelectedApprover4").value = role4;
    }
    __doPostBack("<%=FormApproval1.ClientID %>", '');
}
function SaveFormEditor() {
    document.getElementById("formBuilderData").value = formBuilder.formData;
}

/**
 * ******* WORKFLOW JS
 */
function WorkflowPageLoaded() {
    $('.ui.dropdown')
        .dropdown();
    $('.ui.selection.dropdown').dropdown();
}

/**
 * ******* PROJECT JS
 */
function ProjectPageLoaded() {
    $('.ui.dropdown')
        .dropdown();
}
function saveSelection() {
    var companyEle = document.getElementById("Content_CompanySelect");
    var company = companyEle.options[companyEle.selectedIndex].value;
    var workflowEle = document.getElementById("Content_WorkflowSelect");
    var workflow = workflowEle.options[workflowEle.selectedIndex].value;
    var coachEle = document.getElementById("Content_CoachSelect");
    var coach = coachEle.options[coachEle.selectedIndex].value;
    document.getElementById("Content_SelectedCompany").value = company;
    document.getElementById("Content_SelectedWorkflow").value = workflow;
    document.getElementById("Content_SelectedCoach").value = coach;
}/**
 * ******* ADMIN JS
 */
function AdminPageLoaded() {
    $('.menu .item')
        .tab()
        ;
    $('.ui.dropdown')
        .dropdown()
        ;
}

function saveSelection() {
    var roleEle = document.getElementById("RoleSelect");
    var role = roleEle.options[roleEle.selectedIndex].value;

    var companyEle = document.getElementById("CompanySelect");
    var company = companyEle.options[companyEle.selectedIndex].value;

    var accountEle = document.getElementById("LockedAccountSelect");
    var account = accountEle.options[accountEle.selectedIndex].value;

    document.getElementById("SelectedRole").value = role;
    document.getElementById("SelectedCompany").value = company;
    document.getElementById("SelectedAccount").value = account;

    /* if selected role is not a Client, default company to VC */
    if (role !== 1) {
        document.getElementById("CompanySelect").value = 1;
    }
}
function DisplayDeleteUser() {
    $('.ui.tiny.delete.modal').modal('show');
}

function EditUser(fname, lname, rolename, roleid, userid) {
    $('.ui.small.test.modal').modal('show');

    document.getElementById("user_firstname").value = fname;
    document.getElementById("user_lastname").value = lname;
    document.getElementById("UserRole").value = roleid;
    document.getElementById("UserID").value = userid;
    document.getElementById("UserSelectedRole").value = roleid;
}

function saveUser() {
    var roleEle = document.getElementById("UserRole");
    var role = roleEle.options[roleEle.selectedIndex].value;

    document.getElementById("UserSelectedRole").value = role;
    document.getElementById("UserFirstName").value = document.getElementById("user_firstname").value;
    document.getElementById("UserLastName").value = document.getElementById("user_lastname").value;
}
/**
 * ******* FORMS JS
 */
function FormsPageLoaded() {
    $('.ui.dropdown')
        .dropdown();
    $('.menu .item')
        .tab();
}
function FormBuilder() {
    var viewerOptions = {
        dataType: 'json',
        formData: document.getElementById("formViewerData").value
    };
    var formViewer = $('#renderWrap').formRender(viewerOptions);
    document.getElementById("formViewerData").value = formViewer.formData;

    var builderOptions = {
        dataType: 'json',
        disabledActionButtons: ['data', 'save'],
        controlPosition: 'left',
        formData: document.getElementById("formBuilderData").value
    };
    var formBuilder = $('#buildWrap').formBuilder(builderOptions);
    document.getElementById("formBuilderData").value = formBuilder.formData;
    console.log("FormData: " + formBuilder.formData);
}
function SaveFormViewer() {
    SaveUploadedFiles();
    document.getElementById("formViewerData").value = JSON.stringify(formViewer.userData);
}
function SaveUploadedFiles() {
    var file = $("input:file")[0].files[0];
    var inputName = $("input:file")[0].name;
    if (file) {
        document.getElementById("fileUploadName").value = file.name;
        document.getElementById("fileInputName").value = inputName;
    }
}
function SubmitForm() {
    SaveFormViewer();
    jQuery(function () {
        formRenderOpts = {
            dataType: 'json',
            formData: formViewer.formData
        };
        var renderedForm = $('<div>');
        renderedForm.formRender(formRenderOpts);
        console.log(renderedForm.html());
        document.getElementById("formViewerData").value = renderedForm.html();
    });
}
function ApproveForm() {
    jQuery(function () {
        formRenderOpts = {
            dataType: 'json',
            formData: formViewer.formData
        };
        var renderedForm = $('#renderWrap');
        renderedForm.formRender(formRenderOpts);
        console.log(renderedForm.html());
        document.getElementById("formViewerData").value = renderedForm.html();
    });
}
function saveSelection() {
    console.log("Saving selectors");
    var app1 = document.getElementById("FormApproval1");
    var role1 = app1.options[app1.selectedIndex].value;
    document.getElementById("SelectedApprover1").value = role1;
    var app2 = document.getElementById("FormApproval2");
    if (app2 !== null) {
        var role2 = app2.options[app2.selectedIndex].value;
        document.getElementById("SelectedApprover2").value = role2;
    }
    var app3 = document.getElementById("FormApproval3");
    if (app3 !== null) {
        var role3 = app3.options[app3.selectedIndex].value;
        document.getElementById("SelectedApprover3").value = role3;
    }
    var app4 = document.getElementById("FormApproval4");
    if (app4 !== null) {
        var role4 = app4.options[app4.selectedIndex].value;
        document.getElementById("SelectedApprover4").value = role4;
    }
    __doPostBack("<%=FormApproval1.ClientID %>", '');
}
function SaveFormEditor() {
    document.getElementById("formBuilderData").value = formBuilder.formData;
}

/**
 * ******* WORKFLOW JS
 */
function WorkflowPageLoaded() {
    $('.ui.dropdown')
        .dropdown();
    $('.ui.selection.dropdown').dropdown();
}

/**
 * ******* PROJECT JS
 */
function ProjectPageLoaded() {
    $('.ui.dropdown')
        .dropdown();
}
function saveSelection() {
    var companyEle = document.getElementById("Content_CompanySelect");
    var company = companyEle.options[companyEle.selectedIndex].value;
    var workflowEle = document.getElementById("Content_WorkflowSelect");
    var workflow = workflowEle.options[workflowEle.selectedIndex].value;
    var coachEle = document.getElementById("Content_CoachSelect");
    var coach = coachEle.options[coachEle.selectedIndex].value;
    document.getElementById("Content_SelectedCompany").value = company;
    document.getElementById("Content_SelectedWorkflow").value = workflow;
    document.getElementById("Content_SelectedCoach").value = coach;
}