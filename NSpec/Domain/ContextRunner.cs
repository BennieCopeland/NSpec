﻿using System;
using NSpec.Domain.Formatters;

namespace NSpec.Domain
{
    public class ContextRunner
    {
        public ContextRunner(ContextBuilder builder, IFormatter formatter, bool failFast)
        {
            this.failFast = failFast;
            this.builder = builder;
            this.formatter = formatter;
        }

        public ContextCollection Run(ContextCollection contexts)
        {
            try
            {
                ILiveFormatter liveFormatter = new SilentLiveFormatter();

                if (formatter is ILiveFormatter) liveFormatter = formatter as ILiveFormatter;

                contexts.Run(liveFormatter, failFast);

                if (builder.tagsFilter.HasTagFilters()) contexts.TrimSkippedContexts();

                formatter.Write(contexts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return contexts;
        }

        private ContextBuilder builder;
        private bool failFast;
        private IFormatter formatter;
    }
}