# distutils: sources = ["Decoder_impl.cpp"]
# distutils: language = c++

from libcpp.string cimport string

import numpy as np
cimport numpy as np

cdef extern from "Decoder.h":
	void __decode(const string& str, char* a, char* b)

def decode(string str, np.ndarray a, np.ndarray b):
	return __decode(str, a.data, b.data)