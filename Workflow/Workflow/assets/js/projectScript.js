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
