$(document).ready(function () {
    let table = $("#tonesTable").DataTable({
        language: {
            lengthMenu: "_MENU_",
        }
    });
    
    $(".delete-tone").click(function (){
        const toneId = $(this).attr("data-tone-id");
        $("#modal-delete-btn").click(function (){
            $(this).attr("href", `/delete/${toneId}`)
        });
    });
});

function checkForm() {
    if (!document.getElementById("g-recaptcha-response").value) {
        document.getElementById("reCaptcha-error").innerHTML =
            "Please verify that you're not a robot!";
        return false;
    } else {
        document.getElementById("submit-btn").type = "submit";
        return true;
    }
}