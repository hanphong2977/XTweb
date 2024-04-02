const body = document.querySelector("body"),
    slidebar = body.querySelector(".slidebar"),
    toogle = body.querySelector(".toogle");

    toogle.addEventListener("click", () => {
        slidebar.classList.toggle("close");
    });