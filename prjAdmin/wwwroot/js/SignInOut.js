let btnSignInText = document.querySelector("#btnSignInText"),
    SignInList = document.querySelector("#signinli"),
    SignOutList = document.querySelector("#signoutli");

if (btnSignInText.innerHTML == "登入") {
    SignOutList.setAttribute("style", "display:none");
    SignInList.removeAttribute("style");
}
else {
    SignInList.setAttribute("style", "display:none");
    SignOutList.removeAttribute("style");
}