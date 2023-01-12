using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FarmPoint.Application.Common.Logging;
public static partial class LogMessages
{
    /* TRACE >= 0 */

    /* DEBUG >= 5000 */

    /* INFORMATION >= 10000 */

    /* WARNINGS >= 15000 */

    /* ERRORS >= 20000 */
    [LoggerMessage(20001, LogLevel.Information, "Error creating farm")]
    public static partial void FarmCreatingError(this ILogger logger, Exception exception);

    /* CRITICAL >= 30000 */
}
