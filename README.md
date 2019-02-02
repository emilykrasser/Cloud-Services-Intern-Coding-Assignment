# Cloud Services Intern Coding Assignment
## Emily Krasser - 2019
This is the repository for my solution to the Unity Cloud Services Intern Coding Assignment for the summer of 2019.

## Overview
For this project I used ASP.NET MVC because I really like .NET and C#.
In order to tackle this challenge, I broke down what I needed to do into smaller parts and then came up with methods to go about solving those smaller parts.
1. The first order of operations was to figure out how to get the information from both the Made with Unity page and the various project pages after that. Being as there was no API to call in the source, I retrieved the content via web scraping. The best tool I found to do this was [Html Agility Pack](https://html-agility-pack.net). This allowed me to easily access and store selected Html nodes in a loaded webpage
	- First I retrieved the Html nodes that corresponded to the various projects and their links on the [Made with Unity](https://unity.com/madewith) page and stored them in a list in the HomeController.
		- Note: I cached these URLs server-side due to the information retrieval being quicker than cookies. This cache expires after any information has not been retrieved from it within 8 hours. The drawback with caching is that it stores all the information in the cache on the server, so there is a possibility to exceed storage; however, like I said before, it is faster than cookies.
	- Secondly, I created a Showcase class in the Models folder. It holds five lists to store specific Html nodes in. I was able to glean most of the information off of the project page by searching for nodes that included the tag "col-xs-12". The tough part about this was that it included images with src attributes that held relative paths. Finding these nodes and setting their absolute paths solved the problem. The nodes that were left, were the ones that held the galleries, videos, and background images. These were found via "section-article-gallery", "component-gallery-item", "background-image", "section-trailer embed-responsive".
		- For the background images I had to get the whole image tag as a string and then parse of the URL. In the HTML of the Index page I loaded those absolute paths into an image tag to be displayed at the bottom of the page. The first of the images was also used as the background of each showcase's page.
		- The videos were the last part of the project that I needed to finish and they were the most confusing part since I didn't immediately realize that the video url data was stored in one of the div elements as attributes and had to look at the network calls that were made from those elements. Once I realized that, it was easy enough to set the full link and then embed the video for YouTube and Vimeo.
2. The second part of the project was selecting a random project to show until all the projects had been shown and then repeating this process. I did this by creating a list of numbers that were the indices of the projects that the user had not already seen. I cached these server-side like the URLs. To retrieve an index, a random number was generated within the size of the list of unseen indices and then the corresponding number was returned as well as removed from the cache. Once the cache was depleted, it was refilled and the process continued on. This cache expires after any information has not been retrieved from it within 12 hours.
3. The last bit of the project was just polishing and making it look pretty with some simple CSS. Thanks to Html Agility Pack I was able to preserve a fair amount of original source code from the project website which I could easily format in the Index page.
4. Finally, I wrote a couple of very simple unit tests to help me more rapidly develop and debug the application.

## How the Design Meets the Requirements
The requirements for this project were:
1. A different project must be rendered every time the page is refreshed
2. No project should be repeated until all projects have been viewed by that user
3. The application should contain no content specific to any one game, all discovery and content retrieval should be done through external sources like the Made With Unity showcase
4. User load times and web app network bandwidth usage should be minimized
5. Use one of the following languages: Java, Python, Ruby, Go, C# or C++

How my application meets the requirements:
1. As stated in the Overview, a Made with Unity project URL was chosen by randomly selecting an index from a list of numbers from 0 to the count of the URL list - 1 and then using the returned index to retrieve the URL for the project. That URL was then passed into the ShowcaseRepo.GetShowcaseInfo(URL). This returned a showcase object with all the information that the Index page needed to render.
2. Going right along with requirement number 1, the list of numbers from which a index was selected was the list of all of the project indices that the user had not yet seen. Once an index was chosen, it was removed from the list and the cache was updated. This ensured that no project was repeated until all the projects had already been seen on one pass through. 
3. Being as the application should not contain any project specific information, I turned to the source code. There was no API to cal for the information so I used Html Agility Pack to screen scrape all of the content that I needed to render. This included the title, studio name, headings, text, images, captions, galleries, videos, and background images.
4. In order to minimize the user load times, I used a server-side cache to store both the project URLs as well as the unseen indices. This made it so that the main Html document for the Made with Unity site would only need to be loaded if it was null (that cache expires 8 hours after the last access). The advantage of caching over cookies is that there is less information retrieval, therefore making it faster. If storage were more of an issue, then cookies would be a more valid option. As well, the Html document for each project is only loaded when that project is chosen.
5. For my application, I used ASP.NET MVC since I really like programming in .NET and C#.

## Improvements for the Future and What to do Differently Next Time
I don't think there's much that I would do differently if I had to do over again.
As for improvements for the future:
- I would like to perhaps put more time into maintaining the original format and source of the project site with galleries and videos.
- The type of caching in the application would not work if the site had multiple servers. Instead a distributed cache would need to be used or maybe cookies stored client-side.