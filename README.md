# Discounts
Microservice demonstrating how to handle discounts

- **Discounts rules** use **eligibilty criteria** to determine if a **discount strategy** should be appiled to a **sale** order to calculate a **discount**
 
## Discount Rule
- A discount rule has a **name** used to describe the discount rule on the user interface
- A discount rule has one or more **eligibility criteria**. Each of these eligibility criteria must be met in order for a discount to be applied
- A discount rule has a single **discount strategy** which can be applied to a **sale** in order to calculate the **discount**

## Eligibility Criteria
Eligibility criteria can be applied to a sale in order to determine if a discount should be applied

### Coupon Code
This rule is configured using a simple opaque string as a coupon code
This rule is considered met if a sale includes the configured coupon code

### Date
This rule is configured using a date
This rule is considered met if a sale occurs on the configured date.

### Date Range
This rule is configured using a date rate
This rule is considered met if a sale occurs within the configured date range.

### New Customer
This rule is considered met if a sale is to a new customer

### Existing Customer
This rule is considered met if a sale is to an existing customer

## Discount Strategies

### Flat Amount
The discount is calculated by taking a flat amount off the total

### Flat Percentage
The discount is calculated by taking a flat percentage off the total

### Stepped Percentage
The discount is calculated using a list of thresholds with their corresponding discount percentages
The list is ordered by the thresholds and the first step where the total equals or exceeds the threshold is used.

### Stepped Amount
The discount is calculated using a list of thresholds with their corresponding discount amounts
The list is ordered by the thresholds and the first step where the total equals or exceeds the threshold is used.
