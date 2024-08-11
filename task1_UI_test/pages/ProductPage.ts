import { Page } from '@playwright/test';

export class ProductPage {

  private readonly redTshirtAddButton = '[data-test="add-to-cart-test\\.allthethings\\(\\)-t-shirt-\\(red\\)"]';
  private readonly jacketAddButton = '[data-test="add-to-cart-sauce-labs-fleece-jacket"]';
  private readonly redTshirtRemoveButton = '[data-test="remove-test\\.allthethings\\(\\)-t-shirt-\\(red\\)"]';
  private readonly jacketRemoveButton = '[data-test="remove-sauce-labs-fleece-jacket"]';
  private readonly cartBadge = '.shopping_cart_badge[data-test="shopping-cart-badge"]';
  private readonly cartButton = '[data-test="shopping-cart-link"]';

  constructor(private page: Page) {}

  async addRedTshirtToCart() {
    await this.page.locator(this.redTshirtAddButton).click();
  }

  async addJacketToCart() {
    await this.page.locator(this.jacketAddButton).click();
  }

  async removeRedTshirtFromCart() {
    await this.page.locator(this.redTshirtRemoveButton).click();
  }

  async removeJacketFromCart() {
    await this.page.locator(this.jacketRemoveButton).click();
  }

  async gotoCart() {
    await this.page.locator(this.cartButton).click();
  }

  public get cartBadgeSelector() {
    return this.page.locator(this.cartBadge);
  }

  public get redTshirtRemoveButtonSelector() {
    return this.page.locator(this.redTshirtRemoveButton);
  }

  public get jacketRemoveButtonSelector() {
    return this.page.locator(this.jacketRemoveButton);
  }

  public get jacketAddButtonSelector() {
    return this.page.locator(this.jacketAddButton);
  }

}
