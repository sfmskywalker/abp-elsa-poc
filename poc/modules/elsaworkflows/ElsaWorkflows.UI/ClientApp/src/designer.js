const basePath = '/';

document.addEventListener('DOMContentLoaded', function () {
    Blazor.start({
        loadBootResource: function (type, name, defaultUri, integrity) {
            console.log('loadBootResource', type, name, defaultUri, integrity);
            switch (name) {
                case 'dotnet.js':

                    return `/${defaultUri}`;
            }

            return defaultUri;
        }
    });
});