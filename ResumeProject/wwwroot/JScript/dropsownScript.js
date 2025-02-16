document.addEventListener("DOMContentLoaded", function()
{
  //Select all dropdowns (in case the is more than one)
  var navLinks = document.querySelectorAll(".nav-item a", "nav-item");
  console.log("Found navLinks", navLinks)

  navLinks.forEach(function(link)
  {
    link.addEventListener("mouseenter", function()
    {
        console.log("mouseenter on", link)
        link.classList.add("hover-effect");
    });

    link.addEventListener("mouseleave", function()
    {   
        console.log("mouseleave on", link)
        link.classList.remove("hover-effect");
    });

  });

});