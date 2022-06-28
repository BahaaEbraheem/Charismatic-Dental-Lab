using Charismatic;
using DevExtreme.AspNet.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ITLand.CMMS.Libs.DevExtreme
{
    partial class DataSourceLoadContext
    {
        readonly CharismaticBaseListInputDto _options;
        readonly QueryProviderInfo _providerInfo;
        readonly Type _itemType;

        public DataSourceLoadContext(CharismaticBaseListInputDto options, QueryProviderInfo providerInfo, Type itemType)
        {
            _options = options;
            _providerInfo = providerInfo;
            _itemType = itemType;
        }

        public bool GuardNulls
        {
            get
            {
                return _providerInfo.IsLinqToObjects;
            }
        }

        static bool IsEmptyList(IList list)
        {
            return list == null || list.Count < 1;
        }
    }

    // Filter
    partial class DataSourceLoadContext
    {
        public IList Filter => _options.FilterExpr;
        public bool HasFilter => !IsEmptyList(_options.FilterExpr);
        public bool UseStringToLower => DataSourceLoadOptionsBase.StringToLowerDefault ?? _providerInfo.IsLinqToObjects;
    }


}
