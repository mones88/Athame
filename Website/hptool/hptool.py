import requests
import jinja2
import markdown
from pathlib import Path

ENDPOINT_URL = 'https://api.github.com/repos/svbnet/athame/releases'

def markdownHtml(value):
    return markdown.markdown(value, output_format='html5')


env = jinja2.Environment()
env.filters['markdownHtml'] = markdownHtml

print('Getting releases from GitHub')
releasesStructure = requests.get(ENDPOINT_URL).json()
print('Writing template')
templateContent = Path('index.template.html').read_text()
template = env.from_string(templateContent)
Path('index.html').write_text(template.render(releases=releasesStructure))
