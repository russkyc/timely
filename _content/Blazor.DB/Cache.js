export async function SaveToBrowserCache()
{
    const cache = await caches.open("BlazorDBCache");

    const binaryData = window.Module.FS.readFile("BlazorDatabase.sqlite3");

    const blob = new Blob([binaryData], {
        type: 'application/octet-stream',
        ok: true,
        status: 200
    });

    const headers = new Headers({
        'content-length': blob.size
    });

    const response = new Response(blob, {
        headers
    });

    await cache.put("BlazorDB", response);
}

export async function RestoreFromBrowserCache() {
    const cache = await caches.open("BlazorDBCache");

    const stored = await cache.match("BlazorDB");

    if (stored && stored.ok)
    {
        const binaryData = await stored.arrayBuffer();

        console.log(`Restoring ${binaryData.byteLength} bytes.`);

        window.Module.FS.writeFile("BlazorDatabase.sqlite3", new Uint8Array(binaryData));

        return true;
    }

    return false;
}

export async function DeleteDBFromCache()
{
    const cache = await caches.open("BlazorDBCache");
    cache.delete("BlazorDB");
}

export async function generateDownloadLink(parent, file)
{
    const backupPath = `${file}`;
    const cachePath = `/data/cache/${file.substring(0, file.indexOf('_bak'))}`;
    const db = window.sqlitedb;
    const resp = await db.cache.match(cachePath);

    if (resp && resp.ok) {

        const res = await resp.blob();
        if (res) {
            const a = document.createElement("a");
            a.href = URL.createObjectURL(res);
            a.download = backupPath;
            a.target = "_self";
            a.innerText = `Download ${backupPath}`;
            parent.innerHTML = '';
            parent.appendChild(a);
            return true;
        }
    }

    return false;
}