"use client";

import {
  ActionIcon,
  Badge,
  Button,
  Center,
  Flex,
  Input,
  Loader,
  Menu,
  Select,
  Table,
  Text,
} from "@mantine/core";
import useSWR from "swr";
import { CourseClient, CourseMapping } from "../../web-api-client";
import DashboardLayout from "../../../components/Layout/DashboardLayout";
import Link from "next/link";
import { IconDots } from "@tabler/icons-react";
import { modals } from "@mantine/modals";
import {
  handleSubmit,
  useQuery,
  useSort,
  useFilter,
} from "../../../lib/helper";
import AppPagination from "../../../components/AppPagination/AppPagination";
import { useEffect, useState } from "react";

interface CourseMappingWithIndexSignature extends CourseMapping {
  [key: string]: any;
}

type FilterProps = {
  type: string;
  field: string;
  value: string;
};

const types = [
  { value: "=", label: "Tìm Kiếm Đúng" },
  { value: "@", label: "Tìm Kiếm Gần Đúng" },
  { value: ">", label: "Tìm Kiếm Lớn Hơn" },
  { value: "<", label: "Tìm Kiếm Nhỏ Hơn" },
];

const fields = Object.keys(new CourseMapping().toJSON());

export default function CourseCourse() {
  const CourseService = new CourseClient();
  const query = useQuery();
  const handleSort = useSort();
  const handleFilter = useFilter();

  const [filter, setFilter] = useState<FilterProps>({
    type: types[0].value,
    field: fields[0],
    value: "",
  });

  useEffect(() => {
    if (filter.value) {
      handleFilter("filters", filter.field + filter.type + filter.value);
    } else {
      handleFilter("filters", "");
    }
  }, [filter, handleFilter]);

  const { data, isLoading, mutate } = useSWR(
    `/api/courses/${new URLSearchParams(query as any)}`,
    () =>
      CourseService.getCourses(
        query.filters,
        query.sorts,
        query.page ? parseInt(query.page) : 1,
        query.pageSize ? parseInt(query.pageSize) : 10,
      ),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const handleDelete = (id: number | undefined) => {
    modals.openConfirmModal({
      title: "Xóa Chủ Đề",
      children: (
        <Text size="sm">
          Bạn Chắc Chắn Muốn Xóa? Thao Tác Này Sẽ Không Thể Phục Hồi
        </Text>
      ),
      confirmProps: { color: "red" },
      labels: { confirm: "Chắc Chắn", cancel: "Hủy" },
      onConfirm: () =>
        handleSubmit(() => {
          CourseService.deleteCourse(id).then(() => {
            mutate();
          });
        }, "Xóa Thành Công"),
    });
  };

  return (
    <DashboardLayout>
      <Flex my={"sm"} justify={"end"} align={"center"} gap={"xs"}>
        <Link href={"/dashboard/course"}>
          <Button color="red" size="xs">
            Clear
          </Button>
        </Link>
        <Input
          placeholder="Nhập Nội Dung Tìm Kiếm"
          value={filter.value}
          onChange={(e) =>
            setFilter((prev) => ({ ...prev, value: e.target.value }))
          }
        />
        <Select
          defaultValue={types.find((c) => c.value === filter.type)?.label}
          data={types.map((item) => item.label)}
          onChange={(e) => {
            const selectedType = types.find((c) => c.label === e)?.value;
            if (selectedType) {
              setFilter((prev) => ({
                ...prev,
                type: selectedType,
              }));
            }
          }}
        />
        <Select
          value={filter.field}
          data={fields}
          onChange={(e) => {
            if (e) {
              setFilter((prev) => ({ ...prev, field: e }));
            }
          }}
        />
        <Link href={"/dashboard/course/create"}>
          <Button ms={"auto"} size="xs">
            Tạo Mới
          </Button>
        </Link>
      </Flex>
      <Table layout="fixed">
        <Table.Thead>
          <Table.Tr>
            {fields.map((item) => (
              <Table.Th onClick={() => handleSort(item)} key={item}>
                {item}
              </Table.Th>
            ))}
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>
          {!isLoading ? (
            data?.items?.length && data.items.length >= 0 ? (
              data.items.map((item) => (
                <Table.Tr key={item.name}>
                  {fields.map((field) => (
                    <Table.Td key={field} py={"md"}>
                      {(item as CourseMappingWithIndexSignature)[field]}
                    </Table.Td>
                  ))}
                  <Table.Td>
                    <Menu shadow="md">
                      <Menu.Target>
                        <ActionIcon variant="transparent">
                          <IconDots />
                        </ActionIcon>
                      </Menu.Target>
                      <Menu.Dropdown>
                        <Link
                          href={`/dashboard/course/${item.id}`}
                          style={{ textDecoration: "none" }}
                        >
                          <Menu.Item>Sửa</Menu.Item>
                        </Link>
                        <Menu.Item onClick={() => handleDelete(item.id)}>
                          Xóa
                        </Menu.Item>
                      </Menu.Dropdown>
                    </Menu>
                  </Table.Td>
                </Table.Tr>
              ))
            ) : (
              <Table.Tr>
                <Table.Td colSpan={fields.length}>
                  <Center py={"sm"}>Không Có Gì Ở Đây Cả</Center>
                </Table.Td>
              </Table.Tr>
            )
          ) : (
            <Table.Tr>
              <Table.Td colSpan={fields.length}>
                <Center py={"sm"}>
                  <Loader size={"sm"} />
                </Center>
              </Table.Td>
            </Table.Tr>
          )}
        </Table.Tbody>
      </Table>
      <Flex py={"xs"} justify={"space-between"}>
        <AppPagination page={data?.pageNumber} total={data?.totalPages} />
        <Select
          ms={"auto"}
          data={["5", "10", "15", "20", "25"]}
          defaultValue={query.pageSize?.toString() ?? "10"}
          onChange={(e) => handleFilter("pageSize", e)}
        />
      </Flex>
    </DashboardLayout>
  );
}
