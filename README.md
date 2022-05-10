# Discounts
Microservice demonstrating how to handle discounts

## Eligibility Rules

### Coupon Code
Eligibility is determined by the presence of a correct coupon code

### Date
Eligibility is determined by today's date being a specified date

### Date Range
Eligibility is determined by today's date being within a specified date rangne

### New Customer

### Existing Customer

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
