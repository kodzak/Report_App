
	$(document).ready(function() {
	    $('.menubar > li').bind('mouseover', openSubMenu);
	    $('.menubar > li').bind('mouseout', closeSubMenu);
		
	    function openSubMenu() {
	        $(this).find('ul').css('visibility', 'visible');	
	    };
		
	    function closeSubMenu() {
	        $(this).find('ul').css('visibility', 'hidden');	
	    };
				   
	});
