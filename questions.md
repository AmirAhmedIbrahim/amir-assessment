# Congestion Tax Calculator

For the The single charge rule:

--> If the car passes 3 tolling stations within 60 minutes and then passed another one after 60 minutes,
but the interval between last one and previous one is also during 60 minutes as following
		-- 6:15
		-- 6:30
		-- 6:59
		-- 7:40
How should 7:40 fees calculated? 
I assumed it should be treated as hour and maximum between 6:59 and 7:40 will be charged.
---------------------

--> There is no mention how the user will define the vehicles, I assumed class and worked with it. 
---------------------

--> There is no mention If the required method to caluclate over only one day as per the requirments or cross days as well. 
I assumed cross days and worked with this.
---------------------

Implementation was done to interface not to class, so We can easily change the configurations and caluclations.
----------------------

I noticed the implementation for fees is int, but it should be decimal
I changed this to decimal 

----------------------
Further work but For task scope time:-
- Application could have proper layers, [Web, application, domain, infrastructure]
- Application also could use clean architicure with mediator CQRS, having the logging policy in place.
- I used Vehicle object that is used in application on API project, however should be using dto only. 
- Several Test cases should also added to cover business cases scenarios
----------------------
