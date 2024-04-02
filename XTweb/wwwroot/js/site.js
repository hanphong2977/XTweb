const body = document.querySelector("body"),
    slidebar = body.querySelector(".slidebar"),
    admin_nav = body.querySelector(".admin_nav"),
    toogle = body.querySelector(".toogle");

    toogle.addEventListener("click", () => {
        slidebar.classList.toggle("close");
        admin_nav.classList.toggle("close");
    });