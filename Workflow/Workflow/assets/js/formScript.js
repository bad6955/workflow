/**
 * ******* FORMS JS
 */
function FormsPageLoaded() {

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
