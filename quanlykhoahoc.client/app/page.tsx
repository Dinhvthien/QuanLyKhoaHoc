"use client";

import { Center, Loader, Pagination, SimpleGrid } from "@mantine/core";
import RootLayout from "../components/Layout/RootLayout";
import { CourseCard } from "../components/Card/CourseCard";
import useSWR from "swr";
import { CourseClient } from "./web-api-client";
import { useQuery } from "../lib/helper";
import Loading from "../components/Loading/Loading";
import AppPagination from "../components/AppPagination/AppPagination";

export default function HomePage() {
  const CourseService = new CourseClient();

  const query = useQuery();

  const { data, isLoading } = useSWR(
    `/api/course/${new URLSearchParams(query as any)}`,
    () =>
      CourseService.getCourses(
        query.filters,
        query.sorts,
        query.page ? parseInt(query.page) : 1,
        query.pageSize ? parseInt(query.pageSize) : 9
      )
  );

  return (
    <RootLayout>
      {isLoading ? (
        <Loading />
      ) : (
        data && (
          <>
            <SimpleGrid cols={{ base: 1, lg: 3 }} spacing={"xs"}>
              {data.items.map((item) => {
                return <CourseCard data={item} />;
              })}
            </SimpleGrid>
            <Center my={"md"}>
              <AppPagination page={data.pageNumber} total={data.totalPages} />
            </Center>
          </>
        )
      )}
    </RootLayout>
  );
}
