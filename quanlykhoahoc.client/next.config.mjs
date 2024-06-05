/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: `https://api.nguyenviethaidang.id.vn/api/:path*`,
      },
    ];
  },
};

export default nextConfig;
