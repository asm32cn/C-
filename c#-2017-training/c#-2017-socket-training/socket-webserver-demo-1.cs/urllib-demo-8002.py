#!/usr/bin/env python
# -*- coding: utf-8 -*-

# Python 2.7

import urllib

_url = 'http://localhost:8002/'

for i in xrange(1000):
	print i
	strContent = urllib.urlopen(_url).read()

print 'Done.'
