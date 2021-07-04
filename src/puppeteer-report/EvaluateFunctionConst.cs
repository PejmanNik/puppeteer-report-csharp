namespace PuppeteerReportCsharp
{
    public static class EvaluateFunctionConst
    {

        private static string AsFunction(string body)
        {
            return AsFunction("", body);
        }
        private static string AsFunction(string parameters, string body)
        {
            return $"async ({parameters}) => {{{body}}}";
        }

        public static string Part1(string marginTop, string marginBottom) => AsFunction($@"
            const [getHeightFunc, getHeightArg] = core.getHeightEvaluator(
                ""{marginTop}"",
                ""{marginBottom}""
            );
            const {{ headerHeight, footerHeight }} = await getHeightFunc(getHeightArg);

            const [basePageEvalFunc, basePageEvalArg] = core.getBaseEvaluator(
                headerHeight,
                footerHeight
            );

            await basePageEvalFunc(basePageEvalArg);

            return {{ headerHeight, footerHeight }}; ");

        public static string Part2 => AsFunction("basePdfBuffer", @"
            const [doc, headerEvalFunc, headerEvalArg] = await core.getHeadersEvaluator(
                basePdfBuffer
            );
            await headerEvalFunc(headerEvalArg);
            globalDoc = doc;");

        public static string Part3(string headerHeight, string footerHeight) => AsFunction("headerPdfBuffer", $@"
            return await core.createReport(
                globalDoc,
                headerPdfBuffer,
                {headerHeight},
                {footerHeight}
             );");
    }
}
