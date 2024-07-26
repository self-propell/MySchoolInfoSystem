using Masa.Blazor;
using System.Text.RegularExpressions;
using Util.Reflection.Expressions;

namespace SchPeoManageWeb
{
    public static class Command
    {
        /// <summary>
        /// 邮箱格式正则格式
        /// </summary>
        public const string MailPattern = @"(^$)|(^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$){1,255}";
        /// <summary>
        /// 邮箱格式正则表达式
        /// </summary>
        public static Regex MailRegex = new Regex(Command.MailPattern);
        /// <summary>
        /// 邮箱格式匹配
        /// </summary>
        public static IEnumerable<Func<string, StringBoolean>> MailRule = new List<Func<string, StringBoolean>>
        {
            value => (string.IsNullOrEmpty(value)|| MailRegex.IsMatch(value)) ? true: "请输入正确的联系邮箱"

        };

        /// <summary>
        /// 联系电话正则格式
        /// </summary>
        public static string PhonePattern = @"(^$)|(^1[3-9]\d{9}$)";
        /// <summary>
        /// 联系电话正则表达式
        /// </summary>
        public static Regex PhoneRegex = new Regex(PhonePattern);
        /// <summary>
        /// 联系电话匹配
        /// </summary>
        public static IEnumerable<Func<string, StringBoolean>> PhoneRule = new List<Func<string, StringBoolean>>
        {
            value => (string.IsNullOrEmpty(value) || PhoneRegex.IsMatch(value)) ? true: "请输入正确的联系电话"

        };

        /// <summary>
        /// 身份证正则格式
        /// </summary>
        public static string IDPattern = @"^[1-9]\d{5}(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$";
        /// <summary>
        /// 身份证正则表达式
        /// </summary>
        public static Regex IDRegex = new Regex(IDPattern);
        /// <summary>
        /// 身份证匹配
        /// </summary>
        public static IEnumerable<Func<string, StringBoolean>> IDRule = new List<Func<string, StringBoolean>>
        {
            value => (!string.IsNullOrEmpty(value) && IDRegex.IsMatch(value)) ? true: "请输入正确的身份证号"
        };

        /// <summary>
        /// 教职工号正则格式
        /// </summary>
        public static string EmpIDPattern = @"^T\d{5,11}$";
        /// <summary>
        /// 教职工号正则表达式
        /// </summary>
        public static Regex EmpIDRegex = new Regex(EmpIDPattern);
        /// <summary>
        /// 教职工号匹配
        /// </summary>
        public static IEnumerable<Func<string, StringBoolean>> EmpIDRule = new List<Func<string, StringBoolean>>
        {
            value => (!string.IsNullOrEmpty(value) && EmpIDRegex.IsMatch(value)) ? true: "请输入正确的教职工号"

        };

        /// <summary>
        /// 课程编号正则格式
        /// </summary>
        public static string CoursePMIDPattern = @"^C\d{5,11}$";
        /// <summary>
        /// 课程编号正则表达式
        /// </summary>
        public static Regex CoursePMIDRegex = new Regex(CoursePMIDPattern);
        /// <summary>
        /// 课程编号匹配
        /// </summary>
        public static IEnumerable<Func<string, StringBoolean>> CoursePMIDRule = new List<Func<string, StringBoolean>>
        {
            value => (!string.IsNullOrEmpty(value) && CoursePMIDRegex.IsMatch(value)) ? true: "请输入正确格式课程编号"

        };

        /// <summary>
        /// 课程名正则格式
        /// </summary>
        public static string CourseNamePattern = @"^[\u4e00-\u9fa5A-Za-z0-9\s_\-]+$";
        /// <summary>
        /// 课程名正则表达式
        /// </summary>
        public static Regex CourseNameRegex = new Regex(CourseNamePattern);
        /// <summary>
        /// 课程名匹配
        /// </summary>
        public static IEnumerable<Func<string, StringBoolean>> CourseNameRule = new List<Func<string, StringBoolean>>
    {
        value => (!string.IsNullOrEmpty(value) && CourseNameRegex.IsMatch(value)) ? true: "请输入正确格式课程名"
    };

        /// <summary>
        /// 课时匹配
        /// </summary>
        public static IEnumerable<Func<int, StringBoolean>> ClassHourRule = new List<Func<int, StringBoolean>>
    {
        value => value >= 1 && value <= 96 ? true : "课时超出限制范围【1-96】"
    };

        /// <summary>
        /// 选课人数匹配
        /// </summary>
        public static IEnumerable<Func<int, StringBoolean>> MaxStuNumRule = new List<Func<int, StringBoolean>>
    {
        value => value >= 1 && value <= 3000 ? true : "超出限制范围【1-3000】"
    };

    }
     
}
