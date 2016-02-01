namespace Demo.http
{
    class ServiceUri
    {

        //不知道用来干嘛的(get)大概是确定要请求的东西
        public static readonly string what = "http://139.129.116.86:8000/api/adposlist/android?";


        //版权页(get)
        public static readonly string banquan = "//http://139.129.116.86:8000/api/adlist/android?";

        //文章,现在已经包括了essay,question,serial,每个10篇(get)
        public static readonly string article = "http://139.129.116.86:8000/api/reading/index/?";
        //文章下的评论喜欢按钮(post)
        public static readonly string articleLikeButton = "http://139.129.116.86:8000/api/comment/praise";
        //itemid	1250
        //cmtid	178
        //


        //serial下的,点击文章头查看当前连载列表(目录)
        public static readonly string list = "http://139.129.116.86:8000/api/serial/list/26?";
        //点击转到该连载页面
        //http://139.129.116.86:8000/api/serialcontent/54?
        //目前不清楚这个的目的
        //http://139.129.116.86:8000/api/serialcontent/update/55/2016-01-31%2000:21:23?
        //serial喜欢按钮(post)
        //http://139.129.116.86:8000/api/praise/add
        //包括
        //itemid	54
        //type serial
        //deviceid	00000000-0565-4187-0033-c5870033c587
        //devicetype  android

        //音乐,目前没卵用(get)
        //http://139.129.116.86:8000/api/music/idlist/0?

        //电影
        //http://139.129.116.86:8000/api/movie/list/0?

        //首页,10天(get)
        public static readonly string oneMain = "http://139.129.116.86:8000/api/hp/more/0?";
        //首页喜欢(post)
        public static readonly string oneMainLike = "http://139.129.116.86:8000/api/praise/add";
        //请求体包含:
        //itemid	1237
        //type hpcontent
        //deviceid	00000000-0565-4187-0033-c5870033c587
        //devicetype  android




        //搜索,
        //http://139.129.116.86:8000/api/search/author/Q?
        //http://139.129.116.86:8000/api/search/hp/Q?
        //http://139.129.116.86:8000/api/search/movie/Q?
        //http://139.129.116.86:8000/api/search/music/Q?
        //http://139.129.116.86:8000/api/search/reading/Q?


    }
}
