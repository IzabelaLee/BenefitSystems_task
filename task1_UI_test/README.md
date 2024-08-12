# Automation Test for cart actions on  https://www.saucedemo.com 
To run the tests, type 'npx playwright test' in the terminal in the 'task1_UI_test' directory. You should have Node.js installed.

To see the test results, type 'npx playwright show-report' in the terminal in the same location.

## Technology
TypeScript with the Playwright framework was used to perform the task. I used Visual Studio Code IDE.

## Structure of the project
The root directory of task 1 consists of subdirectories created by default in the playwright project. The code I wrote is located in the 'pages' and 'tests' directories. The files with the classes used for the tests and the test logic are placed there.

## Comments
I was not able to perform in the test "Remove products from the cart" step for checking whether a particular product disappeared from the list. While I know why this happens, at this point I have not come up with a better solution than to check whether the number of products in the list in the cart has decreased by 1.

