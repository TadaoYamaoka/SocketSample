from distutils.core import setup

from Cython.Build import cythonize
import numpy

setup(name='Decorder',
      ext_modules=cythonize("Decoder.pyx"),
      include_dirs = [numpy.get_include()])
