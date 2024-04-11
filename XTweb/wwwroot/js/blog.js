const content = document.getElementById("blog");
async function fetchBlog() {
    const response = await fetch {
        "https://www.googleapis.com/blogger/v3/blogs/3982189192407359422/posts?key=AIzaSyDMlxdQ-2U-feBMPC3aILnm-X1d1hcuMSA"
    };
    const data = await response.json();
    content.innerHTML = data.items[0].content;
}
fetchBlog();