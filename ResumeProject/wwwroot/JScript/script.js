document.addEventListener("DOMContentLoaded", function()
{   
    // Select all elements with the class "dropdown"
    var dropdowns = document.querySelectorAll('.dropdown');

    // For each dropdown, add mouseenter and mouseleave event listeners
    dropdowns.forEach(function(dropdown)
    {
        var menu = dropdown.querySelector('.dropdown-menu');

        dropdown.addEventListener('mouseenter', function()
        {
            menu.classList.add('open');
        });

        dropdown.addEventListener('mouseleave', function()
        {
            menu.classList.remove('open');
        });
    });
});