import urllib2

requset = urllib2.Request('http://www.xxxxx.1com')
try:
    urllib2.urlopen(requset)
except urllib2.URLError, e:
    print e.reason
else:
    print "!OK"

req = urllib2.Request('http://blog.csdn.net/cqcre')
try:
    urllib2.urlopen(req)
except urllib2.HTTPError, e:
    print e.code
    print e.reason

req = urllib2.Request('http://blog.csdn.net/cqcre')
try:
    urllib2.urlopen(req)
except urllib2.URLError, e:
    print "this"
    if hasattr(e,"code"):
        print e.code
    if hasattr(e,"reason"):
        print e.reason
else:
    print "OK"