## Introduction
This project is a solution for a technical assessment from ScanSource where Original project needs to refactor to apply SOLID principles and CQRS pattern on services.

Solution conatains following updates from Original project -
1. Applied SOLID principles
2. Applied CQRS pattern in services
3. Added few validations for the form properties.
4. Added funcationality to UPDATE and DELETE along with ADD.
5. few fixes to accomodate above changes.


## Installation
Step-by-step instructions on how to set up your project locally. Include any prerequisites, such as software or tools that need to be installed.

1. Clone the repository:
    
    git clone https://github.com/pratikbompilwar/platformDeveloperAssessment.git
       
2. Install dependencies (if any):

    Use Nuget package if needed
    
3. Build the project:
    
    Build and Run the project


## Additional Notes

- Added SOLID principles where ever necessary. 
- Refactored code to use CQRS in Repository and Service class. I have not used MediatR as it was not .Net Core project.
- Added Data annotations in the model class and also used server side validation on Form page.
- Customer can be selected from the list and perform update/delete funcationality.
- User can select customer from dropdownlist and data associated with that customer will appear in the form with two buttons for update and    delete.
- Customer Name is used as Primary key and can not be duplicated for implementing update/delete.

## GIT cloning and Repository

- Cloned original project in the local folder using git.
- Created new repository in GITHUB and uploaded local project in new repository using GIT BASH
- commited and push changes to Repo using GIT BASH 
- commands used are "git add .", "git commit -m "message" ", "git push"

## Contact

- Email: pratikbompilwar@yahoo.com
- LinkedIn: https://www.linkedin.com/in/pratik-bompilwar-33937539/
- GitHub: https://github.com/pratikbompilwar
