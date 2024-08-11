import { Page } from '@playwright/test';

export class CartPage {

  private readonly redTshirtTitle = '[data-test="item-3-title-link"]';
  private readonly jacketTitle = '[data-test="item-5-title-link"]';
  private readonly redTshirtRemoveButton = '[data-test="remove-test\\.allthethings\\(\\)-t-shirt-\\(red\\)"]';
  private readonly jacketRemoveButton = '[data-test="remove-sauce-labs-fleece-jacket"]';
  private readonly continueShoppingButton = '[data-test="continue-shopping"]';
  private readonly cartItem = '.cart_item';

  constructor(private page: Page) {}

  async continueShopping() {
    await this.page.locator(this.continueShoppingButton).click();
  }

  public get CartItemsSelector() {
    return this.page.locator(this.cartItem);
  }
  
  public get jacketTitleSelector() {
    return this.page.locator(this.jacketTitle);
  }

  public get redTshirtTitleSelector() {
    return this.page.locator(this.redTshirtTitle);
  }
}
