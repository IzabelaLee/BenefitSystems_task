import { test, expect } from '@playwright/test';
import { LoginPage } from '../pages/LoginPage';
import { ProductPage } from '../pages/ProductPage';
import { CartPage } from '../pages/CartPage';

test.describe('Cart actions', () => {

    let loginPage: LoginPage;
    let productPage: ProductPage;
    let cartPage: CartPage;

     test.beforeEach(async ({ page }) => {
        loginPage = new LoginPage(page);
        productPage = new ProductPage(page);
        cartPage = new CartPage(page);

        await loginPage.goto();
        await loginPage.login('standard_user', 'secret_sauce');  // Exceptional situation, because of public credentials
    });

    test('Add products to cart', async ({ page }) => {
        
        // Log
        await loginPage.goto();
        await loginPage.login('standard_user', 'secret_sauce');

        // Add first product to the cart
        await productPage.addRedTshirtToCart();

        // Check the number next to the cart and rename the button
        await expect(productPage.cartBadgeSelector).toHaveText('1');
        await expect(productPage.redTshirtRemoveButtonSelector).toHaveText('Remove')

        // Check if the product is in the cart
        await productPage.gotoCart();
        await expect(cartPage.redTshirtTitleSelector).toHaveText('Test.allTheThings() T-Shirt (Red)');

        // Continue shopping
        await cartPage.continueShopping();

        // Add the next product 
        await productPage.addJacketToCart();

        // Check the number next to the cart and rename the button
        await expect(productPage.cartBadgeSelector).toHaveText('2');
        await expect(productPage.redTshirtRemoveButtonSelector).toHaveText('Remove')

        // Check if the second product is in the cart
        await productPage.gotoCart();
        await expect(cartPage.jacketTitleSelector).toHaveText('Sauce Labs Fleece Jacket');

        //Check product number of the cart
        await expect(cartPage.CartItemsSelector).toHaveCount(2);
    });

    test('Remove products from cart', async ({ page }) => {
        
        await productPage.addJacketToCart();
        await productPage.addRedTshirtToCart();
        await productPage.gotoCart();

        //Remove product from the cart
        await productPage.removeRedTshirtFromCart();

        //Check the number next to the cart
        await expect(productPage.cartBadgeSelector).toHaveText('1');

        // Check if the product disappeared from the list (no longer to be found on the list) - does not work out
        // const element = await page.locator(cartPage.redTshirtTitle).elementHandle();
        // expect(element).toBeNull();

        await cartPage.continueShopping();

        //Remove a product from the list of products
        await productPage.jacketRemoveButtonSelector.click();

        // Check the number next to the cart and rename the button
        await expect(productPage.cartBadgeSelector).not.toBeVisible();
        await expect(productPage.jacketAddButtonSelector).toHaveText('Add to cart');

    });

});
