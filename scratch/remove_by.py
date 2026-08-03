import os
import re

dir_path = r'c:\Users\USER\Downloads\OCMS'

for root, dirs, files in os.walk(dir_path):
    if '\\bin\\' in root or '\\obj\\' in root or '.git' in root:
        continue
    for file in files:
        if file.endswith('.cs') and file != 'Program.cs':
            filepath = os.path.join(root, file)
            with open(filepath, 'r', encoding='utf-8') as f:
                content = f.read()

            # Delete any line that contains CreatedBy, ModifiedBy, CreateBy, or ModifieBy
            new_content = re.sub(r'^[^\n]*(?:CreatedBy|ModifiedBy|CreateBy|ModifieBy)[^\n]*\n?', '', content, flags=re.MULTILINE|re.IGNORECASE)

            if new_content != content:
                with open(filepath, 'w', encoding='utf-8') as f:
                    f.write(new_content)
                print(f"Updated {filepath}")
