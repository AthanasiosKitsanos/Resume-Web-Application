document.addEventListener("DOMContentLoaded", function()
{
    var gearIcon = document.querySelector('.gear-icon');

    var gearDropdownMenu = document.querySelector('.gear-dropdown-menu');

    if(gearIcon && gearDropdownMenu)
    {
        function openDropdown()
        {
            gearDropdownMenu.classList.add('open');
            gearIcon.style.transform = 'rotate(180deg)';
        }

        function closeDropdown()
        {
            gearDropdownMenu.classList.remove('open');
            gearIcon.style.transform = 'rotate(0deg)';
        }

        // Gear Icon
        gearIcon.addEventListener('mouseenter', openDropdown);

        gearIcon.addEventListener('mouseleave', function()
        {
            setTimeout(function()
            {
                if(!gearDropdownMenu.matches(':hover'))
                {
                    closeDropdown();
                }
            }, 100);
        });

        // Gear Dropdown Menu
        gearDropdownMenu.addEventListener('mouseenter', openDropdown);

        gearDropdownMenu.addEventListener('mouseleave', function()
        {
            setTimeout(function()
            {
                if(!gearIcon.matches(':hover'))
                {
                    closeDropdown();
                }
            }, 100);
        });
    }

    var dropdown = document.querySelector('.dropdown');

    var dropdownMenu = document.querySelector('.dropdown-menu');

    if(dropdown && dropdownMenu)
    {
        function openDropdown()
        {
            dropdownMenu.classList.add('open');
        }

        function closeDropdown()
        {
            dropdownMenu.classList.remove('open');
        }

        // Dropdown
        dropdown.addEventListener('mouseenter', openDropdown);

        dropdown.addEventListener('mouseleave', function()
        {
            setTimeout(function()
            {
                if(!dropdownMenu.matches(':hover'))
                {
                    closeDropdown();
                }
            }, 100);
        });

        // Dropdown Menu
        dropdownMenu.addEventListener('mouseenter', openDropdown);

        dropdownMenu.addEventListener('mouseleave', function()
        {
            setTimeout(function()
            {
                if(!dropdown.matches(':hover'))
                {
                    closeDropdown();
                }
            }, 100);
        });
    }
});