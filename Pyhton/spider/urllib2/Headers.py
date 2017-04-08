import urllib
import urllib2

url = 'https://www.zhihu.com'
user_agent = 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36'
values = {'username': 'cqc', 'password': 'XXXX'}
headers = {'User-Agent': user_agent , 'Referer':'http://www.zhihu.com/articles'}
data = urllib.urlencode(values)
request = urllib2.Request(url, data, headers)
response = urllib2.urlopen(request)
page = response.read()