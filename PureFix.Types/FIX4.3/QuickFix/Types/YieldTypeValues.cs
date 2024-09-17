namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class YieldTypeValues
	{
		public const string TrueYieldTheYieldCalculatedWithCouponDatesMovedFromAWeekendOrHolidayToTheNextValidSettlementDate = "TRUE";
		public const string PreviousCloseYieldTheYieldOfABondBasedOnTheClosingPrice1DayAgo = "PREVCLOSE";
		public const string YieldToLongestAverage = "LONGEST";
		public const string YieldToLongestAverageLifeTheYieldAssumingOnlyMandatorySinksAreTakenThisResultsInALowerPaydownOfDebtTheYieldIsThenCalculatedToTheFinalPaymentDate = "LONGAVGLIFE";
		public const string YieldToMaturityTheYieldOfABondToItsMaturityDate = "MATURITY";
		public const string MarkToMarketYieldAnAdjustmentInTheValuationOfASecuritiesPortfolioToReflectTheCurrentMarketValuesOfTheRespectiveSecuritiesInThePortfolio = "MARK";
		public const string OpenAverageYieldTheAverageYieldOfTheRespectiveSecuritiesInThePortfolio = "OPENAVG";
		public const string YieldToNextPutTheYieldToTheDateAtWhichTheBondHolderCanNextPutTheBondToTheIssuer = "PUT";
		public const string ProceedsYieldTheCdEquivalentYieldWhenTheRemainingTimeToMaturityIsLessThanTwoYears = "PROCEEDS";
		public const string SemiAnnualYieldTheYieldOfABondWhoseCouponPaymentsAreReinvestedSemiAnnually = "SEMIANNUAL";
		public const string YieldToShortestAverageLifeSameAsAvglifeAbove = "SHORTAVGLIFE";
		public const string YieldToShortestAverage = "SHORTEST";
		public const string SimpleYieldTheYieldOfABondAssumingNoReinvestmentOfCouponPayments = "SIMPLE";
		public const string YieldToTenderDateTheYieldOnAMunicipalBondToItsMandatoryTenderDate = "TENDER";
		public const string YieldValueOf132TheAmountThatTheYieldWillChangeForA132ndChangeInPrice = "VALUE1/32";
		public const string YieldToWorstConventionTheLowestYieldToAllPossibleRedemptionDateScenarios = "WORST";
		public const string TaxEquivalentYieldTheAfterTaxYieldGrossedUpByTheMaximumFederalTaxRateOf396ForComparisonToTaxableYields = "TAXEQUIV";
		public const string AnnualYieldTheAnnualInterestOrDividendIncomeAnInvestmentEarnsExpressedAsAPercentageOfTheInvestmentsTotalValue = "ANNUAL";
		public const string ClosingYieldMostRecentYearTheYieldOfABondBasedOnTheClosingPriceAsOfTheMostRecentYearsEnd = "LASTYEAR";
		public const string YieldToNextRefund = "NEXTREFUND";
		public const string AfterTaxYield = "AFTERTAX";
		public const string YieldAtIssue = "ATISSUE";
		public const string YieldToAverageLifeTheYieldAssumingThatAllSinks = "AVGLIFE";
		public const string YieldToAverageMaturityTheYieldAchievedBySubstitutingABondsAverageMaturityForTheIssuesFinalMaturityDate = "AVGMATURITY";
		public const string BookYieldTheYieldOfASecurityCalculatedByUsingItsBookValueInsteadOfTheCurrentMarketPriceThisTermIsTypicallyUsedInTheUsDomesticMarket = "BOOK";
		public const string YieldToNextCallTheYieldOfABondToTheNextPossibleCallDate = "CALL";
		public const string YieldChangeSinceCloseTheChangeInTheYieldSinceThePreviousDaysClosingYield = "CHANGE";
		public const string CompoundYieldTheYieldOfCertainJapaneseBondsBasedOnItsPriceCertainJapaneseBondsHaveIrregularFirstOrLastCouponsAndTheYieldIsCalculatedCompoundForTheseIrregularPeriods = "COMPOUND";
		public const string CurrentYieldAnnualInterestOnABondDividedByTheMarketValueTheActualIncomeRateOfReturnAsOpposedToTheCouponRateExpressedAsAPercentage = "CURRENT";
		public const string TrueGrossYieldYieldCalculatedUsingThePriceIncludingAccruedInterestWhereCouponDatesAreMovedFromHolidaysAndWeekendsToTheNextTradingDay = "GROSS";
		public const string GovernmentEquivalentYieldAskYieldBasedOnSemiAnnualCouponsCompoundingInAllPeriodsAndActualActualCalendar = "GOVTEQUIV";
		public const string YieldWithInflationAssumptionBasedOnPriceTheReturnAnInvestorWouldRequireOnANormalBondThatWouldMakeTheRealReturnEqualToThatOfTheInflationIndexedBondAssumingAConstantInflationRate = "INFLATION";
		public const string InverseFloaterBondYieldInverseFloaterSemiAnnualBondEquivalentRate = "INVERSEFLOATER";
		public const string ClosingYieldMostRecentQuarterTheYieldOfABondBasedOnTheClosingPriceAsOfTheMostRecentQuartersEnd = "LASTQUARTER";
		public const string MostRecentClosingYieldTheLastAvailableYieldStoredInHistoryComputedUsingPrice = "LASTCLOSE";
		public const string ClosingYieldMostRecentMonthTheYieldOfABondBasedOnTheClosingPriceAsOfTheMostRecentMonthsEnd = "LASTMONTH";
		public const string ClosingYieldTheYieldOfABondBasedOnTheClosingPrice = "CLOSE";
	}
}
