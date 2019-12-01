import os

for root, dirs, files in os.walk('./'):
    for name in files:
        if name.endswith((".log")):
            trace_file = name.replace('.log', '.trace')
            os.system('python trace.py ' + name + ' > ' + trace_file)