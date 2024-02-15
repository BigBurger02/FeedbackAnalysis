# Overview

**Project Name:** Feedback Analysis

**Description:** web application that collects customer feedback and analyses the sentiment using Azure AI Services.

# Architecture
I used client-server architecture to divide server logic and user interface.

# Technologies
- Backend/API
	- ASP.NET 8
	- PostgreSQL hosted on a free provider
	- Entity Framework
	- Azure Language Service as AI to analyse data
- Frontend/Client
	- .NET Blazor WebAssembly
	- Bootstrap for CSS
- Azure Language Service
	- Sentiment analysis

# Design Decisions

## API Design
Endpoint api/feedback to save feedback in the database and analyse it using AI, then returns analyse information with sent feedback.
### Model:
- FeedbackMessage that contains info provided by the client (message, userId, etc.)
- AzureLanguageAnalyseInfo that contains overall info about the analysis
- AzureLanguageAnalyseSubjectOpinion that contains a "key-value" pair about some subjects that were mentioned in the feedback
- FeedbackWithAnalysis used to pass data to the client and contains FeedbackMessage, AzureLanguageAnalyseInfo, and collection of AzureLanguageAnalyseSubjectOpinion
### Database tables
- Feedbacks
- AzureLanguageAnalyseInfo
- AzureLanguageAnalyseSubjectOpinion

## Client Design
I used Blazor to provide real-time updates in the UI when new feedback was analysed.
Bootstrap helped me to make a sleek form as fast as possible.

## Azure Language Service
Of all the opportunities that Azure AI Services provide, I chose Language service because it has the ability to do sentiment analysis and opinion mining. `Azure.AI.TextAnalytics` library has a convenient interface to easily interact with the Service.
# Challenges Faced

## Dive into Azure AI Services and decide what service I need
I have a specific task and have to find what service I should use to solve this task. After reading official documentation, I understand that I need Azure Language Service to mine text for clues about positive or negative sentiment.

## Handle analysis
After getting the data analysis I had to structure information into a readable form that I could provide to the Client. I also had to design database tables to efficiently store analysis.
I decided to track the overall sentiment and score of the feedback and store opinion sentiment about each subject specified in the feedback.

## Develop Client on Blazor
I haven't worked with Blazor for a very long time, so I had to remember how to do basic things there.

# Testing
API was tested with Postman. You can check [Postman collection](https://documenter.getpostman.com/view/28242426/2sA2r6WPDq) demonstrating API functionality.

# Validation
Validation of user input takes place on both the server and the client. 
I made custom validation:
- `Validate()` method in the blazor page
- validation of provided data in the action of API controller. Server returns 400_BadRequest with error description if validation falls

# Deployment
[API](https://devrain-api.azurewebsites.net/swagger/index.html) and [Client](https://devrain-client.azurewebsites.net/) hosted on Azure.
